using System;
using System.Collections.Generic;
using System.Text;

namespace Z.Fantasy.GenerateCode.Models;

/// <summary>
/// 保存文件
/// </summary>
public class FileModel
{
    /// <summary>
    /// 生成的内容
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// 生成的路径
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 文件名 如：user.cs
    /// </summary>
    public string FullName { get; set; }
}