using System.Configuration;

/// <summary>
/// ConfigUtil 的摘要说明
/// 用于陪住系统全局Cofnig变量及方法
/// </summary>
public class ConfigUtil
{
    public ConfigUtil() { }


    #region 配置全局Config方法

    /// <summary>
    /// 获取Web.Config配置的数据库连接
    /// </summary>
    /// <param name="key">配置的数据库连接名称</param>
    /// <returns></returns>
    public static string GetConnectionString(string key)
    {
        return ConfigurationManager.ConnectionStrings[key].ConnectionString;
    }

    /// <summary>
    /// 获取Web.Config  AppSettings 配置节的信息
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetAppSettings(string key)
    {
        string text = ConfigurationManager.AppSettings[key];
        if (text == null)
        {
            text = string.Empty;
        }
        return text;
    }

    /// <summary>
    /// 获取Web.Config  AppSettings 配置节的信息, 提供默认值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string GetAppSettings(string key, string defaultValue)
    {
        try
        {
            object text = ConfigurationManager.AppSettings[key];
            return (text == null) ? defaultValue : (string)text;
        }
        catch
        {
            return defaultValue;
        }
    }
    #endregion
}