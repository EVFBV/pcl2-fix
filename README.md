# PCL Fix

基于官方 **Plain Craft Launcher 2 快照版 2.14.0.0 (412)** 的补丁版，通过 **IL 定点补丁**（Mono.Cecil）实现——不重新编译、不改动任何 XAML/BAML 资源。

> **当前版本：v2.0.0** → [下载](https://github.com/EVFBV/pcl2-fix/releases/tag/v2.0.0)
>
> `PCL Fix.exe` · 5,987,840 字节 · SHA-256 `30409829802AFAB1F04AB83FB897DC3D6070A4A443D5213B80E5557AD8113806`

---

## 本版新增（v2.0.0）

- **联机页解锁**：解除 PCL 对联机功能的两道本地封锁，导航栏恢复「启动 / 下载 / 联机 / 设置 / 更多」
- **BLFP 联机服务集成分析**：[docs/05-BLFP联机集成.md](docs/05-BLFP联机集成.md)（协议解析 + 两条落地路线 + 当前阻塞点）
- 附带独立小工具 **`PCL联网下载解锁.exe`**：不替换 PCL，直接给现有的 PCL 打补丁（快照版/正式版通用）

---

## 全部改动

### 品牌

| # | 位置 | 改动 |
|---|---|---|
| 1 | `FormMain..ctor` | 窗口/任务栏标题 → `PCL Fix` |
| 2 | `FormMain.UpdateBackgroundAndTitleBar` | 标题栏图形 Logo → 文字 `PCL Fix` |
| 3 | `PageOtherAbout_Loaded` | 关于页产品名 → `PCL Fix` |
| 4 | `Application.WaitingMutex` | 多开定位用的窗口名常量 |
| 5 | 程序集属性 / Win32 版本资源 | Title / Product / Description / FileDescription / ProductName / OriginalFilename |

### 去限制

| # | 位置 | 改动 | 效果 |
|---|---|---|---|
| 6 | `ModSecret.ServerSub` | 注入钩子跳过 `isUpdate` 公告 | 「悲报：不再提供 MC 下载功能」弹窗不再出现 |
| 7 | `PageLaunchRight.Init` | `PanHint` → `Collapsed` | 启动页「快照版 PCL…」提示条隐藏 |
| 8 | `PageSetupSystem_Loaded` | `PanDonate` → `Collapsed` | 「复制识别码 / 输入土豆码」整行隐藏 |
| 9 | `PageSetupSystem_Loaded` | `CheckSystemTelemetry` → `Collapsed` | 「匿名数据收集」勾选框隐藏 |
| 10 | `ModMain.Telemetry` | 方法体清空 | 匿名上报功能整体失效 |
| 11 | `ModSecret.UpdateCheckByButton` | 文案替换 | 「由于此版本的特殊性,暂时不提供在线更新功能」 |

### 联机（v2.0.0 新增）

| # | 位置 | 改动 | 效果 |
|---|---|---|---|
| 12 | `Settings.Get` 的 `ForcedSettings` | 键名改写 | 不再强制隐藏联机页 / 联机设置页 |
| 13 | `PageSetupUI.HiddenRefresh` | `BtnTitleSelect2` 硬编码 `Collapsed` → `Visible` | 导航栏「联机」恢复显示 |
| 14 | `PageLinkMain.InitConfig` | 不再读 `DisableReason2` | 不再弹「联机功能已暂时关闭」 |

> 第 14 项解除后，PCL 自带的 EasyTier 联机流程可以正常跑（建房 → 邀请码 → 好友加入）。
> 与 BLFP 服务端的对接状态、阻塞点与后续路线见 [docs/05-BLFP联机集成.md](docs/05-BLFP联机集成.md)。

---

## 目录

```
patcher/           PCL Fix 补丁器源码（C# + Mono.Cecil）
unlocker/          PCL联网下载解锁 源码（net48 + Costura 单文件）
docs/
  01-架构分析.md        技术栈、分层、启动流程、子系统、数据布局
  02-破解分析.md        上游破解版 4 处改动的逐行对照与二进制取证
  03-类型与成员索引.md   137 个类型的行数 / 方法数 / 位置
  04-重建与环境.md       环境、依赖、从零重建工程的步骤
  05-BLFP联机集成.md     BLFP 协议解析、PCL 联机实现对照、两条落地路线
  06-解锁工具说明.md     PCL联网下载解锁.exe 的使用与改动说明
src/               ILSpy 反编译得到的 C# 源码（仅供研究，版权归原作者）
screenshots/       修改后效果截图
修改说明.md         逐条改动的说明与验证记录
```

---

## 构建

### PCL Fix（需要原版 exe + 依赖 DLL）

```powershell
dotnet build .\patcher -c Release

.\patcher\bin\Release\net8.0\PclFixPatcher.exe `
    "原版 Plain Craft Launcher 2 2.14.0.0.exe" `
    "输出\PCL Fix.exe" `
    "依赖 DLL 目录"
```

依赖 DLL 内嵌在原 exe 的 `PCL.Resources.resources` 里（`MeloongCore`、`MeloongCore.Wpf`、`PCLCS`、`Newtonsoft.Json`、`NAudio`、`CacheCow.*`、`Imazen.WebP`、`ThrottleDebounce`、`Ookii.Dialogs.Wpf` 等），导出后加 `.dll` 后缀即可。

### PCL联网下载解锁（单文件，.NET Framework 4.8）

```powershell
dotnet build .\unlocker -c Release
# 产物：unlocker\bin\Release\PCL联网下载解锁.exe
```

---

## 已知限制

1. **「检查更新」不可用**：原版的 `UpdateStart` 已被上游破解者掏空，本补丁不重建下载器，只把按钮文案改成中性提示
2. **BLFP 服务未直连**：BLFP 的建房/加入接口需要账号登录 + GeeTest 验证码 + HMAC 请求签名，未登录访问返回 401；详见 docs/05
3. **程序集名仍是 `Plain Craft Launcher 2`**：XAML 的 `pack://application:,,,/Plain Craft Launcher 2;component/...` 依赖它，改名会导致界面资源全部加载失败；互斥体也仍是 `PCL_SingletonMutex`，与原版不能同时运行
4. **联机页的公告类通知**：跳过的是所有 `isUpdate=true` 的公告，活动类公告仍会正常显示

---

## 声明

- `src/` 目录为 ILSpy 对官方发行版的反编译结果，**版权归原作者 龙腾猫跃 / Meloong 所有**，仅作研究与学习用途
- `patcher/`、`unlocker/`、`docs/` 为本仓库作者产出
- 请勿用于商业用途或二次分发官方本体；如需完整功能请支持正版 PCL：<https://github.com/Meloong-Git/PCL>
