using Fanex.AccInfo.Business.Models;

namespace Fanex.AccInfo.Business.Services;

public static class AppConfigService
{
    public static ConfigSettingModel Config { get; set; }

    public static bool IsProcessing { get; set; }
}