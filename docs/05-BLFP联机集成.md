# BLFP 联机服务集成分析

> 目标仓库：<https://github.com/blfp-team/blfp-client>（BLFP Client，Electron + EasyTier + frp）
> 目标程序：PCL 2.14.0.0 快照版（`PageLinkMain` + EasyTier）

---

## 一、结论先说

| 项 | 状态 |
|---|---|
| PCL 联机页的两道**本地**封锁 | **已解除**（导航按钮硬隐藏、`DisableReason2` 拦截），PCL 自带 EasyTier 联机恢复可用 |
| BLFP 的**服务端**房间 API | **无法直接接入**：需要 BLFP 账号登录 + GeeTest 验证码 + HMAC 请求签名，未登录访问 `/api/rooms/public` 直接 401 |
| 能否"PCL 建房 → BLFP 好友加入" | 目前不能。两边都是 EasyTier，但**网络名/密码/中继节点**由各自服务端按房间签发，互不通用 |

一句话：**PCL 侧的锁已经打开了，BLFP 侧的钥匙（账号/API 授权）不在本地。**

---

## 二、BLFP 客户端技术画像

从源码看（`main.js` 48 KB、`renderer/app.js` 193 KB、`src/easytier-manager.js` 20 KB）：

- **形态**：Electron 桌面客户端，Windows/Linux 双平台
- **组网**：EasyTier（`bin/easytier-core.exe` + `easytier-cli.exe` + `wintun.dll` + `WinDivert64.sys`）
- **备用通道**：frp（`bin/frpc.exe`），固定公网节点 + 随机隧道名
- **房间**：6 位房间号，房主创建即得，好友输入加入
- **玩法**：加入者在 Minecraft 里直连房主虚拟地址（`10.200.X.1:25565`）

也就是说 **BLFP 和 PCL 用的是同一套底层（EasyTier）**，差别只在"谁来分配网络名/密码/中继节点"。

---

## 三、BLFP 服务端接口实测

| 地址 | 用途 | 实测 |
|---|---|---|
| `https://p.blfp.cn` | 主 API（生产） | `/api/health` → `200 {"ok":true,"ready":true,...}` |
| `http://154.40.43.136:4000` | 主 API（直连） | `/api/health` → `200` |
| `http://154.40.43.136:4001` | 聊天/信令侧 | `/api/health` → `200 {"service":"chat",...}` |

### 鉴权链

1. `POST /api/auth/login`（账号密码 / 邮箱验证码 / TFA）—— **带 GeeTest v4 验证码**（`static.geetest.com/v4/gt4.js`）
2. 登录后拿到 `token`，之后所有请求带 `Authorization: Bearer <token>`
3. **写请求还要做 HMAC-SHA256 签名**：

```
GET  /api/auth/signing-key        -> 拿到 hex 签名密钥
payload = method \n path \n timestamp \n nonce \n sha256(body)
X-Signature = HMAC-SHA256(key, payload)   # hex
X-Timestamp / X-Nonce 一并带上
```

4. 未登录访问 `/api/rooms/public` → **401**（已实测）

### 信令（WebSocket）

```
wss://{host}/ws?token={token}
→ {"type":"create","mode":"easytier","username":...,"userId":...,"mcPort":25565,"isPublic":false}
← {"type":"created","room":"123456","easytier":{"networkName":...,"networkSecret":...,"peers":[...],"hostVirtualIp":"10.200.x.1"},"maxMembers":N}
→ {"type":"join","room":"123456"}
← {"type":"joined","easytier":{...}}
```

客户端拿到 `easytier` 后自己拉起 `easytier-core`，把 `mcPort` 反代到虚拟网卡上。

---

## 四、PCL 侧联机实现（对照）

| 项 | PCL 2.14 | BLFP |
|---|---|---|
| 底层 | EasyTier | EasyTier |
| 房间凭据来源 | **本地生成**（随机网络名 + 密码 + 发现节点 ID） | **服务端签发**（每房间临时凭据） |
| 邀请码 | `{网络名}-{密码}-2{3位hex节点ID}`（`PageLinkMain.GetInviteCode`） | 6 位数字房间号 |
| 中继节点 | `ServerConfig.Link.Peers`（PCL 自己的 EasyTier 节点） | BLFP 共享节点（`resolveEtPeers` 解析） |
| 开关 | `ServerConfig.Link.DisableReason2` 非空 → `InitConfig` 抛异常 | — |
| 导航入口 | `PageSetupUI.HiddenRefresh` 里 `BtnTitleSelect2.Visibility = Collapsed` **硬隐藏** | — |

**关键差异**：PCL 的房间是"离线"的——网络名/密码由房主本地生成，只靠邀请码传递；BLFP 的房间是"在线"的——凭据由服务端签发、有过期与成员管理。所以**两者不能直接互通**，除非其中一方改用对方的凭据来源。

---

## 五、两条可落地路线

### 路线 A（推荐，需要 BLFP 官方配合）

让 BLFP 暴露一个**免验证码的房间接口**（例如 `POST /api/partner/rooms`，用 API Key 代替登录），返回 `{room, easytier:{networkName, networkSecret, peers, hostVirtualIp}}`。

PCL 侧只需一处改动：把 `PageLinkMain` 的建房/加入改成调这个接口，邀请码换成 6 位房间号。
工作量小、体验最好，但**必须 BLFP 团队点头**。

### 路线 B（本地桥，不需要 BLFP 改）

写一个 `BLFP-PCL 联机桥`：

1. 用 BLFP 账号登录（**需要人工过一次 GeeTest 验证码**，或复用 BLFP 客户端已登录的 token）
2. 走信令建房 / 加入，拿到 `easytier` 配置
3. 用这份配置拉起 `easytier-core`，把 `mcPort` 反代到虚拟网卡
4. 把结果告诉用户：**在 PCL 里正常启动游戏，好友在多人游戏页输入 `10.200.x.1:25565`**

这条路 **不需要改 PCL 的联机代码**——PCL 只负责启动游戏，组网交给桥。缺点是必须装有 BLFP 账号，且首次要过验证码。

> 本次没有实现路线 B 的原因：验证码 + HMAC 签名 + 账号三件套无法在无人值守环境完成验证，
> 交付未经真机验证的联网代码风险更高。等拿到 BLFP 的 partner API（路线 A）或一个可用账号，可以很快补上。

---

## 六、本次实际交付的改动

在 `PCL Fix` 与 `PCL联网下载解锁.exe` 里都加了这 3 处补丁，把 PCL 自带联机的两道本地封锁拆掉：

| 位置 | 原始行为 | 补丁后 |
|---|---|---|
| `Settings.Get` 的 `ForcedSettings` | 强制 `UiHiddenPageLink`/`UiHiddenSetupLink = true` | 键名改写，强制项失效 |
| `PageSetupUI.HiddenRefresh` | `BtnTitleSelect2.Visibility = Collapsed`（联机按钮永远不显示） | 改为 `Visible` |
| `PageLinkMain.InitConfig` | 读 `ServerConfig.Link.DisableReason2`，非空就抛"联机功能已暂时关闭" | 改读 `DisableReason2#unlocked`（读不到 → 不抛） |

效果（已实测）：导航栏恢复 `启动 / 下载 / 联机 / 设置 / 更多`，点进联机页不再弹"已暂时关闭"，PCL 自己的 EasyTier 建房/加入流程可以正常跑。

---

## 七、如果要继续推进

需要以下任意一项，我就能把 BLFP 真正接进来：

1. **BLFP partner API**（推荐）：一个免验证码的建房/加入接口 + API Key
2. **一个可用的 BLFP 账号**（用户名密码，且允许我在本机过一次验证码）
3. **BLFP 客户端已登录后的 token**（`localStorage.mclink_token`），有效期内的 token 可直接调信令

拿到任一项后，路线 A 大约 1 天、路线 B 大约 2 天可以出可测版本。
