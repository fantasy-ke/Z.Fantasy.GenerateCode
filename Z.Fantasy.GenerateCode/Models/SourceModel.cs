using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;

namespace Z.Fantasy.GenerateCode.Models;

/// <summary>
/// 数据源实体
/// </summary>
public class SourceModel
{
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; }
    /// <summary>
    /// 表的中文名（表描述）
    /// </summary>
    public string TableNameDes { get; set; }
    /// <summary>
    /// 数据库类型：PgSql;MySql;SqlServer;Oracle
    /// </summary>
    public string DbType { get; set; }
    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string ModelNamespace { get; set; }
    /// <summary>
    /// 验证类命名空间
    /// </summary>
    public string ModelValidatorNamespace { get; set; }
    /// <summary>
    /// 实体名称
    /// </summary>
    public string ModelName { get; set; }
    
    /// <summary>
    /// IdType
    /// </summary>
    public string IdType { get; set; }
    
    /// <summary>
    /// 实体名称
    /// </summary>
    public string CamelModelName { get; set; }
    /// <summary>
    /// 字段列表
    /// </summary>
    public List<Field> FieldList { get; set; }
    /// <summary>
    /// 服务层命名空间
    /// </summary>
    public string CoreNamespace { get; set; }
    
    /// <summary>
    /// 服务层命名空间
    /// </summary>
    public string AppNamespace { get; set; }
    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string ControllerNamespace { get; set; }

    /// <summary>
    /// 字段
    /// </summary>
    public class Field 
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段名称（中文名）
        /// </summary>
        public string NameDes { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 对应C#类型
        /// </summary>
        public string CSharp { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public string Length { get; set; }
        /// <summary>
        /// 是否为空：是；否
        /// </summary>
        public string IsNull { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region 前端使用
        /// <summary>
        /// 序号
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 列标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// 对应Ts类型
        /// </summary>
        public string TsType { get; set; }
        #endregion
    }

    #region 验证器
    /// <summary>
    /// 验证器
    /// </summary>
    public class Validator : AbstractValidator<SourceModel>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Validator()
        {
            RuleFor(m => m.FieldList).NotNull();
        }
    }
    /// <summary>
    /// 开始验证参数，抛出异常形式
    /// </summary>
    public void Validate()
    {
        Validator validator = new Validator();
        ValidationResult results = validator.Validate(this);
        string allMessages = results.ToString();//换行符分隔
        if (!string.IsNullOrWhiteSpace(allMessages))
        {
            throw new Exception(allMessages);
        }
    }
    #endregion

    /// <summary>
    /// 深度克隆
    /// </summary>
    /// <returns></returns>
    public SourceModel DeepClone()
    {
        return JsonSerializer.Deserialize<SourceModel>(JsonSerializer.Serialize(this));
    }
}