# 🧨Z.Fantasy.GenerateCode

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download)
[![WPF UI](https://img.shields.io/badge/WPF%20UI-3.0.4-brightgreen)](https://wpfui.lepo.co/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE.txt)
<img src="https://badges.toozhao.com/badges/01JG0Z40FFFNXNM8NVHBANX84K/green.svg" />

## 📝 项目简介

C# 代码生成器是一个基于 WPF 和 WPF UI 的桌面应用程序，旨在通过 T4 模板语言实现自动化代码生成。用户可以根据需求自由修改项目模板，通过导入数据表结构的 Excel 文件，一键生成所需的代码文件。

## ✨ 主要特性

- 🚀 基于 .NET 8.0 和 WPF UI 框架开发
- 📊 支持 Excel 数据表结构导入
- 🎨 提供美观的现代化界面
- 🌈 支持浅色/深色主题切换
- 🔧 可自定义 T4 模板
- 🎯 支持多种编程语言代码生成

## 🛠️ 安装使用

### 环境要求

- Windows 操作系统
- .NET 8.0 SDK 或更高版本
- Visual Studio 2022 或更高版本（用于开发）

### 快速开始

1. **获取项目**
   ```powershell
   git clone https://github.com/your-username/Z.Fantasy.GenerateCode.git
   cd Z.Fantasy.GenerateCode
   ```

2. **编译运行**
   - 使用 Visual Studio 打开解决方案文件 `Z.Fantasy.GenerateCode.sln`
   - 编译并运行项目

3. **使用步骤**
   1. 准备数据表结构的 Excel 文件（参考项目中的`模板表数据源.xlsx`）
   2. 在应用程序中导入 Excel 文件
   3. 选择需要使用的代码模板
   4. 配置生成选项
   5. 点击生成按钮，获取生成的代码文件

## 📚 模板使用指南

### Excel 文件格式要求

- 表格必须包含字段名、数据类型、描述等基本信息
- 支持的数据类型参考示例文件
- 确保 Excel 文件格式规范，避免特殊字符

### T4 模板开发

1. **模板位置**
   - 模板文件存放在 `Template` 目录下
   - 按项目类型分类存放，便于管理

2. **模板开发规范**
   - 遵循 T4 模板语法规范
   - 建议在修改模板前先备份
   - 可参考项目提供的示例模板

## 🖼️ 界面预览

### 主页
![主页界面](https://github.com/user-attachments/assets/d94bbbdf-f833-4410-ba8e-b0bced940ce4)

### 代码生成
![代码生成](https://github.com/user-attachments/assets/a5213dad-e18b-44f1-b90a-c30a55151bf5)

### 生成结果
![生成结果](https://github.com/user-attachments/assets/46b54814-1c78-4bd2-95de-aa8550d41be0)

### 主题设置
![主题设置](https://github.com/user-attachments/assets/f5ecea4c-d123-415e-acf2-cbca5283ee1d)

## 🔧 技术栈

- **UI 框架**：WPF + [WPF UI](https://wpfui.lepo.co/)
- **模板引擎**：T4 (Text Template Transformation Toolkit)
- **Excel 处理**：[EPPlus](https://www.epplussoftware.com/)
- **MVVM 框架**：[CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction)

## 📌 注意事项

1. Excel 文件格式必须严格按照模板要求设计
2. 修改 T4 模板前建议先进行备份
3. 生成的代码可能需要根据实际项目需求进行调整
4. 建议定期更新到最新版本以获取新功能和修复

## 🤝 贡献指南

欢迎提交 Issue 和 Pull Request 来帮助改进项目。在提交之前，请确保：

1. Issue 描述清晰，包含复现步骤
2. Pull Request 遵循项目代码规范
3. 更新相关文档和注释

## 📄 开源协议

本项目基于 [MIT 协议](LICENSE.txt) 开源，欢迎自由使用和分享。

## 🙏 鸣谢

- [WPF UI](https://wpfui.lepo.co/) - 现代化的 WPF UI 框架
- [EPPlus](https://www.epplussoftware.com/) - Excel 处理库
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/introduction) - MVVM 框架
