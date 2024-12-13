using Z.Fantasy.GenerateCode.Models;
using Z.Fantasy.GenerateCode.Template.SunBlog.Application;
using Z.Fantasy.GenerateCode.Template.SunBlog.Core;

namespace Z.Fantasy.GenerateCode.Services;

public static class GenerateTextHelper
{
    
    public static readonly List<string> IgnoreColumn =
        ["CreatorId", "CreationTime", "Id", "DeletionTime", "DeleterId", "IsDeleted"];
    #region T4模板生成器
    public static string GenerateInterfaceManager(SourceModel req)
    {            
        var data =req.DeepClone();
        //过滤指定字段不生成
        data.FieldList.RemoveAll((m) => IgnoreColumn.Contains(m.Name));
        //调用T4模板生成内容
        var model = new InterfaceDomainManager(data);
        var content = model.TransformText();
        return content;
    }
        
    public static string GenerateAchieveManager(SourceModel req)
    {            
        var data =req.DeepClone();
        //过滤指定字段不生成
        data.FieldList.RemoveAll((m) => IgnoreColumn.Contains(m.Name));
        //调用T4模板生成内容
        var model = new AchieveDomainManager(data);
        var content = model.TransformText();
        return content;
    }
        
    /// <summary>
    /// 生成实体类
    /// </summary>
    public static string GenerateModel(SourceModel req)
    {            
        var data =req.DeepClone();
        //过滤指定字段不生成
        data.FieldList.RemoveAll((m) => m.Name != "Id" && IgnoreColumn.Contains(m.Name));
        //调用T4模板生成内容
        ModelTemplate model = new ModelTemplate(data);
        string content = model.TransformText();
        return content;
    }
    
    /// <summary>
    /// 生成InputDto类
    /// </summary>
    public static string GenerateCreateOrUpdateInput(SourceModel req)
    {            
        var data =req.DeepClone();
        //调用T4模板生成内容
        var model = new CreateOrUpdateInput(data);
        string content = model.TransformText();
        return content;
    }
    
    /// <summary>
    /// 生成AutoMapperConfig类
    /// </summary>
    public static string GenerateAutoMapperConfig(SourceModel req)
    {            
        var data =req.DeepClone();
        //调用T4模板生成内容
        var model = new AutoMapperConfig(data);
        string content = model.TransformText();
        return content;
    }
    
    
    /// <summary>
    /// 生成AchieveAppService类
    /// </summary>
    public static string GenerateAchieveService(SourceModel req)
    {            
        var data =req.DeepClone();
        //调用T4模板生成内容
        var model = new AchieveAppService(data);
        string content = model.TransformText();
        return content;
    }
    
    /// <summary>
    /// 生成InterfaceAppService类
    /// </summary>
    public static string GenerateInterfaceService(SourceModel req)
    {            
        var data =req.DeepClone();
        //调用T4模板生成内容
        var model = new InterfaceAppService(data);
        string content = model.TransformText();
        return content;
    }
    
    #endregion
}