using Fanex.AccInfo.Business.Models;

namespace Fanex.AccInfo.Business.Services;

public static class AppConfigService
{
    public static ConfigSettingModel Config { get; set; } = new ConfigSettingModel();
}