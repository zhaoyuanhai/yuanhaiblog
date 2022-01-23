using BootstrapAdmin.DataAccess.Models;
using BootstrapAdmin.Web.Core;
using BootstrapBlazor.Components;
using Longbow.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BootstrapAdmin.DataAccess.EFCore.Services;

class DictService : IDict
{
    private IDbContextFactory<BootstrapAdminContext> DbFactory { get; set; }
    private Caching.ICacheManager _cacheManager { get; set; }
    private string AppId { get; set; }
    private const string DictServiceCacheKey = "DictService-GetAll";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="configuration"></param>
    public DictService(IDbContextFactory<BootstrapAdminContext> factory, IConfiguration configuration, Caching.ICacheManager cacheManager)
    {
        DbFactory = factory;
        AppId = configuration.GetValue("AppId", "BA");
        _cacheManager = cacheManager;
    }

    public List<Dict> GetAll()
    {
        using var context = DbFactory.CreateDbContext();
        return context.Dicts.ToList();
    }

    public Dictionary<string, string> GetApps()
    {
        var dicts = GetAll();
        return dicts.Where(d => d.Category == "应用程序").Select(d => new KeyValuePair<string, string>(d.Code, d.Name)).ToDictionary(i => i.Key, i => i.Value);
    }

    public Dictionary<string, string> GetLogins()
    {
        var dicts = GetAll();
        return dicts.Where(d => d.Category == "系统首页").Select(d => new KeyValuePair<string, string>(d.Code, d.Name)).ToDictionary(i => i.Key, i => i.Value);
    }

    public Dictionary<string, string> GetThemes()
    {
        var dicts = GetAll();
        return dicts.Where(d => d.Category == "网站样式").Select(d => new KeyValuePair<string, string>(d.Code, d.Name)).ToDictionary(i => i.Key, i => i.Value);
    }

    public string GetWebFooter()
    {
        var dicts = GetAll();
        var title = "网站页脚";
        var name = dicts.FirstOrDefault(d => d.Category == "应用程序" && d.Code == AppId)?.Name;
        if (!string.IsNullOrEmpty(name))
        {
            var dict = dicts.FirstOrDefault(d => d.Category == name && d.Name == "网站页脚") ?? dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "网站页脚");
            title = dict?.Code ?? "网站标题";
        }
        return title;
    }

    public string GetWebTitle()
    {
        var dicts = GetAll();
        var title = "网站标题";
        var name = dicts.FirstOrDefault(d => d.Category == "应用程序" && d.Code == AppId)?.Name;
        if (!string.IsNullOrEmpty(name))
        {
            var dict = dicts.FirstOrDefault(d => d.Category == name && d.Name == "网站标题") ?? dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "网站标题");
            title = dict?.Code ?? "网站标题";
        }
        return title;
    }

    public bool IsDemo()
    {
        var dicts = GetAll();
        var code = dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "演示系统" && d.Define == EnumDictDefine.System)?.Code ?? "0";
        return code == "1";
    }

    public bool SaveDemo(bool isDemo)
    {
        throw new NotImplementedException();
    }

    public bool AuthenticateDemo(string code)
    {
        var ret = false;
        if (!string.IsNullOrEmpty(code))
        {
            var dicts = GetAll();
            var salt = dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "授权盐值" && d.Define == EnumDictDefine.System)?.Code;
            var authCode = dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "哈希结果" && d.Define == EnumDictDefine.System)?.Code;
            if (!string.IsNullOrEmpty(salt))
            {
                ret = LgbCryptography.ComputeHash(code, salt) == authCode;
            }
        }
        return ret;
    }

    public bool SaveHealthCheck(bool enable = true)
    {
        return true;
    }

    public int GetCookieExpiresPeriod()
    {
        var dicts = GetAll();
        var code = dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "Cookie保留时长" && d.Define == EnumDictDefine.System)?.Code ?? "0";
        _ = int.TryParse(code, out var ret);
        return ret;
    }

    public string GetCurrentLogin()
    {
        var dicts = GetAll();
        return dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "登录界面" && d.Define == EnumDictDefine.System)?.Code ?? "Login";
    }

    private bool SaveDict(Dict dict)
    {
        var dbContext = DbFactory.CreateDbContext();
        dbContext.Dicts.Where(x => x.Name == dict.Name && x.Category == dict.Category).ToList().ForEach(item =>
        {
            item.Code = dict.Code;
        });
        var ret = dbContext.SaveChanges() >= 1;
        if (ret)
        {
            // 更新缓存
            _cacheManager.Clear(DictServiceCacheKey);
        }
        return ret;
    }

    public bool SaveLogin(string login)
    {
        return SaveDict(new Dict { Category = "网站设置", Name = "登录界面", Code = login });
    }

    public bool SaveTheme(string theme) => SaveDict(new Dict { Category = "网站设置", Name = "使用样式", Code = theme });

    public bool SaveWebTitle(string title) => SaveDict(new Dict { Category = "网站设置", Name = "网站标题", Code = title });

    public bool SaveWebFooter(string footer) => SaveDict(new Dict { Category = "网站设置", Name = "网站页脚", Code = footer });

    public bool SaveCookieExpiresPeriod(int expiresPeriod) => SaveDict(new Dict { Category = "网站设置", Name = "Cookie保留时长", Code = expiresPeriod.ToString() });

    public string? GetProfileUrl(string appId) => GetUrlByName(appId, "个人中心地址");

    public string? GetSettingsUrl(string appId) => GetUrlByName(appId, "系统设置地址");

    public string? GetNotificationUrl(string appId) => GetUrlByName(appId, "系统通知地址");

    public bool SavDefaultApp(bool enabled) => SaveDict(new Dict
    {
        Category = "网站设置",
        Name = "默认应用程序",
        Code = enabled ? "1" : "0",
        Define = EnumDictDefine.System
    });

    public string RetrieveIconFolderPath()
    {
        var dicts = GetAll();
        return dicts.FirstOrDefault(d => d.Name == "头像路径" && d.Category == "头像地址" && d.Define == EnumDictDefine.System)?.Code ?? "images/uploder/";
    }

    public string? GetHomeUrlByAppId(string? appId = null)
    {
        string? url = null;
        var dicts = GetAll();

        // 查看是否开启默认应用
        var enableDefaultApp = GetEnableDefaultApp();
        if (enableDefaultApp)
        {
            // appId 为空时读取前台列表取第一个应用作为默认应用
            appId ??= GetApps().FirstOrDefault(d => d.Key != AppId).Key ?? AppId;
            url = dicts.FirstOrDefault(d => d.Category == "应用首页" && d.Name.Equals(appId, StringComparison.OrdinalIgnoreCase) && d.Define == EnumDictDefine.System)?.Code;
        }
        return url;
    }

    public bool GetEnableDefaultApp()
    {
        var dicts = GetAll();
        var code = dicts.FirstOrDefault(d => d.Category == "网站设置" && d.Name == "默认应用程序")?.Code ?? "0";
        return code == "1";
    }

    private string? GetUrlByName(string appId, string dictName)
    {
        string? url = null;
        var dicts = GetAll();
        var appName = dicts.FirstOrDefault(d => d.Category == "应用程序" && d.Code == appId && d.Define == EnumDictDefine.System)?.Name;
        if (!string.IsNullOrEmpty(appName))
        {
            url = dicts.FirstOrDefault(d => d.Category == appName && d.Name == dictName && d.Define == EnumDictDefine.Customer)?.Code;
        }
        return url;
    }
}
