using Fanex.AccInfo.Business.Models;

namespace Fanex.AccInfo.Business.Services
{
    public static class AccInfoConfigService
    {
        public static ConfigSettingModel Config { get; set; } = new ConfigSettingModel();
    }
}