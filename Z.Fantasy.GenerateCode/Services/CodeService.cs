using System.IO;
using System.Text.Json;
using OfficeOpenXml;
using Z.Fantasy.GenerateCode.Models;

namespace Z.Fantasy.GenerateCode.Services;

/// <summary>
/// 代码生成管理
/// </summary>
public class CodeService
{
    private readonly LogHelper _log = new LogHelper();

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="contentList"></param>
    private List<string> SaveFile(List<FileModel> contentList)
    {
        List<string> list = new List<string>();
        foreach (var item in contentList)
        {
            if (!Directory.Exists(item.Path))
                Directory.CreateDirectory(item.Path);
            System.IO.File.WriteAllText($"{item.Path}\\{item.FullName}", item.Content);

            list.Add($"{item.Path}\\{item.FullName}");
        }

        return list;
    }

    /// <summary>
    /// 生成所有
    /// </summary>
    /// <param name="req"></param>
    public void GenerateAll(SourceModel req)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory + "\\src";
        var backCorePath = $"{path}\\后台代码\\Core";
        var backAppPath = $"{path}\\后台代码\\Application";
        var fontPath = $"{path}\\前台代码";
        var sqlPath = $"{path}\\SQL语句";
        var langPath = $"{path}\\国际化模板";

        switch (req.DbType.Trim().ToLower())
        {
            case "oracle":
                throw new Exception("暂不支持：oracle 敬请期待！");
            default:
                break;
        }

        //转换数据类型
        ConvertDataType(req);

        _log.Debug("获取到的数据源");
        _log.Debug(JsonSerializer.Serialize(req));

        var modelPath = $"{backCorePath}\\{req.ModelName}Module";
        var mangerPath = $"{modelPath}\\DomainManager";
        var servicePath = $"{backAppPath}\\{req.ModelName}Module";
        var dtoPath = $"{backAppPath}\\{req.ModelName}Module\\Dto";
        var mapperConfigPath = $"{backAppPath}\\{req.ModelName}Module\\MapperConfig";
        List<FileModel> list =
        [
            new()
            {
                Content = GenerateTextHelper.GenerateModel(req),
                Path = modelPath,
                FullName = $"{req.ModelName}.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateInterfaceManager(req),
                Path = mangerPath,
                FullName = $"I{req.ModelName}Manager.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateAchieveManager(req),
                Path = mangerPath,
                FullName = $"{req.ModelName}Manager.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateCreateOrUpdateInput(req),
                Path = dtoPath,
                FullName = $"CreateOrUpdate{req.ModelName}Input.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateAutoMapperConfig(req),
                Path = mapperConfigPath,
                FullName = $"{req.ModelName}AutoMapper.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateInterfaceService(req),
                Path = servicePath,
                FullName = $"I{req.ModelName}AppService.cs"
            },

            new()
            {
                Content = GenerateTextHelper.GenerateAchieveService(req),
                Path = servicePath,
                FullName = $"{req.ModelName}AppService.cs"
            }
        ];

        SaveFile(list);
    }

    /// <summary>
    /// 导入Excel数据源
    /// </summary>
    public static List<SourceModel> ImportExcel(string fileName)
    {
        var sourceModelList = new List<SourceModel>();

        //指定EPPlus使用非商业证书
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var file = new FileInfo(fileName);
        using var package = new ExcelPackage(file);
        //索引从0开始
        //var sheet = package.Workbook.Worksheets[0];
        foreach (var sheet in package.Workbook.Worksheets)
        {
            SourceModel sourceModel = new SourceModel();
            sourceModel.FieldList = new List<SourceModel.Field>();

            int colCount = sheet.Dimension.End.Column;
            int rowCount = sheet.Dimension.End.Row;

            #region 获取表基本信息

            sourceModel.TableName = sheet.Cells[1, 2].Value?.ToString();
            sourceModel.TableNameDes = sheet.Cells[1, 6].Value?.ToString();
            sourceModel.DbType = sheet.Cells[1, 8].Value?.ToString();
            sourceModel.ModelNamespace = sheet.Cells[2, 2].Value?.ToString();
            sourceModel.ModelName = sheet.Cells[2, 6].Value?.ToString();
            sourceModel.CamelModelName = ConvertToCamelCase(sourceModel.ModelName);

            #endregion

            #region 获取字段列表

            for (var r = 5; r <= rowCount; r++)
            {
                var field = new SourceModel.Field();
                if (string.IsNullOrWhiteSpace(sheet.Cells[r, 1].Value?.ToString()))
                {
                    continue;
                }

                for (var c = 1; c <= colCount; c++)
                {
                    var value = sheet.Cells[r, c].Value?.ToString();
                    switch (c)
                    {
                        case 1:
                            field.Name = value;
                            break;
                        case 2:
                            field.NameDes = value;
                            break;
                        case 3:
                            field.Type = value;
                            break;
                        case 4:
                            field.Length = value;
                            break;
                        case 5:
                            field.IsNull = value;
                            break;
                        case 6:
                            field.Remark = value;
                            break;
                        case 7:
                            field.Sort = null;
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                field.Sort = Convert.ToInt32(value);
                            }

                            break;
                        case 8:
                            field.Title = value;
                            break;
                        case 9:
                            field.Width = value;
                            break;
                    }
                }

                sourceModel.FieldList.Add(field);
            }

            #endregion

            sourceModelList.Add(sourceModel);
        }

        return sourceModelList;
    }


    static string ConvertToCamelCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }

    #region 数据库类型 转 C#类型

    /// <summary>
    /// 转换数据类型
    /// </summary>
    private void ConvertDataType(SourceModel req)
    {
        var dic = new Dictionary<string, string>();
        var tsDic = TsDataType();
        switch (req.DbType.Trim().ToLower())
        {
            case "mysql":
                dic = MySqlDataType();
                break;
            case "pgsql":
                dic = PgSqlDataType();
                break;
            case "sqlserver":
                dic = SqlServerDataType();
                break;
            case "oracle":
            default:
                break;
        }

        req.FieldList.ForEach(item =>
        {
            item.CSharp = dic[item.Type.Trim().ToLower()];
            if (req.DbType.Trim().ToLower() == "mysql" && item.Length != null && item.Type == "char" &&
                int.Parse(item.Length) == 36)
            {
                item.CSharp = "Guid";
            }

            if (!string.IsNullOrEmpty(item.IsNull) && item.CSharp != "string")
            {
                if (item.IsNull.Trim() == "是")
                {
                    item.CSharp += "?";
                }
            }

            item.TsType = tsDic[item.Type.Trim().ToLower()];
        });
        req.IdType = req.FieldList[0].CSharp;
    }

    /// <summary>
    /// PgSql对应C#类型
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> PgSqlDataType()
    {
        var data = new Dictionary<string, string>
        {
            { "int", "int" },
            { "decimal", "decimal" },
            { "float", "float" },
            { "double", "double" },
            { "numerc", "decimal" },
            { "char", "string" },
            { "varchar", "string" },
            { "nvarchar", "string" },
            { "text", "string" },
            { "datetime", "DateTime" },
            { "date", "DateTime" },
            { "time", "DateTime" },
            { "timestamp", "byte[]" },
            { "bit", "bool" },
            { "bool", "bool" },
            { "enum", "这类型不知绑什么" },
            { "blob", "string" },
            { "smallint", "int" },
            { "bigint", "long" },
            { "tinyint", "int" },
            { "integer", "int" },
            { "json", "string" },
            { "xml", "string" }
        };

        return data;
    }

    /// <summary>
    /// MySql对应C#类型
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> MySqlDataType()
    {
        var data = new Dictionary<string, string>
        {
            { "int", "int" },
            { "decimal", "decimal" },
            { "float", "float" },
            { "double", "double" },
            { "char", "string" },
            { "varchar", "string" },
            { "text", "string" },
            { "datetime", "DateTime" },
            { "date", "DateTime" },
            { "time", "DateTime" },
            { "bit", "bool" },
            { "bool", "bool" },
            { "blob", "string" },
            { "smallint", "int" },
            { "bigint", "long" },
            { "tinyint", "int" },
            { "integer", "int" },
            { "json", "string" }
        };

        return data;
    }

    /// <summary>
    /// SqlServer 对应C#类型
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string> SqlServerDataType()
    {
        var data = new Dictionary<string, string>
        {
            { "bigint", "long" },
            { "binary", "byte[]" },
            { "bit", "bool" },
            { "char", "string" },
            { "date", "DateTime" },
            { "datetime", "DateTime" },
            { "datetime2", "DateTime" },
            { "datetimeoffset", "DateTimeOffset" },
            { "decimal", "decimal" },
            { "float", "double" },
            { "int", "int" },
            { "money", "decimal" },
            { "nchar", "string" },
            { "numeric", "decimal" },
            { "nvarchar", "string" },
            { "smalldatetime", "DateTime" },
            { "smallint", "short" },
            { "smallmoney", "decimal" },
            { "text", "string" },
            { "time", "TimeSpan" },
            { "timestamp", "byte[]" },
            { "tinyint", "byte" },
            { "varbinary", "byte[]" },
            { "varchar", "string" },
            { "uniqueidentifier", "Guid" },
            { "xml", "Xml" }
        };
        return data;
    }

    private Dictionary<string, string> TsDataType()
    {
        var data = new Dictionary<string, string>
        {
            { "bigint", "number" },
            { "bit", "boolean" },
            { "char", "string" },
            { "uniqueidentifier", "string" },
            { "date", "string" },
            { "datetime", "Date" },
            { "datetime2", "Date" },
            { "datetimeoffset", "Date" },
            { "decimal", "number" },
            { "float", "number" },
            { "int", "number" },
            { "money", "number" },
            { "nchar", "string" },
            { "numeric", "number" },
            { "nvarchar", "string" },
            { "smalldatetime", "string" },
            { "smallint", "number" },
            { "smallmoney", "number" },
            { "text", "string" },
            { "time", "Date" },
            { "varchar", "string" },
            { "xml", "string" },
            { "double", "number" },
            { "bool", "boolean" },
            { "blob", "string" },
            { "tinyint", "number" },
            { "integer", "number" },
            { "json", "string" },
            { "numerc", "number" },
            { "enum", "number" },
        };
        return data;
    }

    #endregion
}