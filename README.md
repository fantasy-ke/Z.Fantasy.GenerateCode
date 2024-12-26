# 🧨Z.Fantasy.GenerateCode <img src="https://badges.toozhao.com/badges/01JG0Z40FFFNXNM8NVHBANX84K/green.svg" />

## 🎃net8.0-windows  WPF框架

## 描述
C# 代码生成器是一个基于 WPF 和 WPF UI 的桌面应用程序，旨在通过 T4 模板语言实现自动化代码生成。用户可以根据需求自由修改项目模板，通过导入数据表结构的 Excel 文件，一键生成所需的代码文件。

---

## 目的
C# 代码生成器的主要目的是简化开发流程，减少重复性工作，提升开发效率。它适用于以下场景：

- 根据数据库表结构生成实体类、数据访问层代码
- 快速搭建基础 CRUD 功能
- 为大型项目生成统一的代码模板，确保一致性
- 降低手动编写代码的错误概率

---

## 功能
### 核心功能
1. **Excel 数据导入**
   - 支持用户上传数据表结构的 Excel 文件。
   - 自动解析表结构并映射为生成模板的输入。

2. **T4 模板支持**
   - 允许用户基于 T4 模板语言自定义生成逻辑。
   - 提供示例模板供快速上手，涵盖实体类、服务层、控制器等多种场景。

3. **界面设计**
   - 使用现代化的 WPF UI 提供友好的用户体验。
   - 支持表结构预览、模板编辑和生成结果查看。

4. **多语言支持**
   - 根据项目需求，支持生成 C#、TypeScript、JavaScript 等语言代码。

5. **代码输出**
   - 生成的代码支持直接保存到指定目录。
   - 提供文件结构预览，方便项目集成。

---

### 主页

- ![image-20241226173118680](https://github.com/user-attachments/assets/d94bbbdf-f833-4410-ba8e-b0bced940ce4)


### 生成

- 生成 导入 模板数据原
  - ![image-20241226173140419](https://github.com/user-attachments/assets/a5213dad-e18b-44f1-b90a-c30a55151bf5)


- 打开生成目录
  - 可以看到生成的文件，以及项目机构。
  - ![image-20241226173249653](https://github.com/user-attachments/assets/46b54814-1c78-4bd2-95de-aa8550d41be0)


### 设置

- 设置`Light` 跟`Dark`两种主题
- ![image-20241226173149169](https://github.com/user-attachments/assets/f5ecea4c-d123-415e-acf2-cbca5283ee1d)


---

## 技术栈
- 前端界面：WPF + WPF UI
- 模板语言：T4 (Text Template Transformation Toolkit)
- 数据导入：EPPlus 或类似库解析 Excel 文件

---

## 注意事项
1. 确保 Excel 文件的表结构格式与模板要求一致。
2. 在修改 T4 模板时，建议保留备份以避免错误。
3. 生成的代码文件需根据实际项目需求进行二次调整。

## 🍟感谢

- [WPF UI](https://wpfui.lepo.co/index.html)
  - Nuxt是一个 开源框架 ，使得Web开发变得直观且强大。可以自信地创建高性能和生产级别的全栈Web应用和网站。
