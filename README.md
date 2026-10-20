# PCL Fix

基于官方 **Plain Craft Launcher 2 快照版 2.14.0.0 (412)** 的修改版，通过 **IL 定点补丁**（Mono.Cecil）实现，不重新编译、不改动任何 XAML/BAML 资源。

> 产物：`PCL Fix.exe`（5,987,840 字节，SHA-256 `F1560FE3437E7AAB254EC1E162311222595481EAC6FF2B6D9E2B068F8C64FC6F`）

---

## 改动一览

| # | 位置 | 改动 | 效果 |
|---|---|---|---|
| 1 | `ModSecret.ThemeCheckOne` | 恒返回 `true`（沿用上游破解版） | 全部主题无条件解锁 |
| 2 | `ModSecret.ServerSub` | 注入 `PCLFix.Hooks.NeutralizeIfUpdate`，`isUpdate=true` 的公告删掉 `id` 后被跳过 | 「悲报 / PCL 不再提供 MC 下载功能」弹窗不再出现 |
| 3 | `PageLaunchRight.Init` | 追加 `PanHint.Visibility = Collapsed` | 启动页「快照版 PCL 包含尚未正式发布的测试功能…」提示条隐藏 |
| 4 | `PageSetupSystem.PageSetupSystem_Loaded` | `PanDonate.Visibility` 由 `Visible` 改为 `Collapsed` | 「复制识别码 / 输入土豆码」整行隐藏 |
| 5 | `PageSetupSystem.PageSetupSystem_Loaded` | 追加 `CheckSystemTelemetry.Visibility = Collapsed` | 「匿名数据收集」勾选框隐藏 |
| 6 | `ModMain.Telemetry` | 方法体清空 | 匿名数据上报功能整体失效（启动/退出/下载失败/崩溃/联机） |
| 7 | `ModSecret.UpdateCheckByButton` | 弹窗文案替换 | 「由于此版本的特殊性,暂时不提供在线更新功能」 |
| 8 | `FormMain..ctor` | `Title = "PCL Fix"` | 窗口 / 任务栏标题 |
| 9 | `FormMain.UpdateBackgroundAndTitleBar` | 强制文字 Logo | 标题栏左上角显示「PCL Fix」 |
| 10 | `PageOtherAbout_Loaded` | `ItemAboutPcl.Title = "PCL Fix"` | 关于页产品名 |
| 11 | `Application.WaitingMutex` | `FindWindow` 窗口名常量 | 多开时能定位已开窗口 |
| 12 | 程序集属性 / Win32 版本资源 | Title / Product / Description / FileDescription / ProductName / OriginalFilename | 文件属性显示 PCL Fix |

完整说明见 [修改说明.md](修改说明.md)。

---

## 目录

```
patcher/          补丁器源码（C# + Mono.Cecil，.NET 8）
docs/             逆向分析文档
  01-架构分析.md      技术栈、分层、启动流程、子系统、数据布局
  02-破解分析.md      上游破解版 4 处改动的逐行对照与二进制取证
  03-类型与成员索引.md 137 个类型的行数 / 方法数 / 位置
  04-重建与环境.md     环境、依赖、从零重建工程的步骤
src/              ILSpy 反编译得到的 C# 源码（仅供研究，版权归原作者）
screenshots/      修改后效果截图
修改说明.md        本次全部改动的逐条说明与验证记录
```

---

## 构建补丁

```powershell
dotnet build .\patcher -c Release

.\patcher\bin\Release\net8.0\PclFixPatcher.exe `
    "原版 Plain Craft Launcher 2 2.14.0.0.exe" `
    "输出\PCL Fix.exe" `
    "依赖 DLL 目录"
```

依赖 DLL 目录用于 Cecil 解析 `PCLCS` 等引用。这些 DLL 内嵌在原 exe 的 `PCL.Resources.resources` 里，可用 `System.Resources.ResourceReader` 导出后加 `.dll` 后缀：

```
MeloongCore  MeloongCore.Wpf  PCLCS  Newtonsoft.Json  NAudio
CacheCow.Client  CacheCow.Client.FileStore  CacheCow.Common
Imazen.WebP  ThrottleDebounce  Ookii.Dialogs.Wpf
Microsoft.Win32.Registry  System.Net.Http.Formatting
```

要在原版 PCL 同时运行的情况下测试，加 `--test-mutex`（互斥体名改为 `PCLFix_TestMutex`、窗口名改为 `PCL Fix Test`）。

---

## 已知副作用

1. 程序集名仍为 `Plain Craft Launcher 2`（XAML 的 `pack://application:,,,/Plain Craft Launcher 2;component/...` 依赖它），互斥体仍为 `PCL_SingletonMutex` —— **与原版不能同时运行**，并共用注册表设置 `HKCU\Software\PCL` 与缓存目录
2. 跳过的是所有 `isUpdate=true` 的公告（不只是「悲报」），活动类公告仍正常显示
3. 关闭匿名上报后作者收不到错误日志，属于预期行为

---

## 声明

- `src/` 目录为 ILSpy 对官方发行版的反编译结果，**版权归原作者 龙腾猫跃 / Meloong 所有**，仅作研究与学习用途
- `patcher/` 与 `docs/` 为本仓库作者产出
- 请勿用于商业用途或二次分发官方本体；如需完整功能请支持正版 PCL：<https://github.com/Meloong-Git/PCL>
