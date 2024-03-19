using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Models;

public class ConfigSettingModel
{
    public string SentryClientKey { get; set; }

    public string CategorySentryLog { get; set; }

    public string AccFile { get; set; }

    public string ApiKey { get; set; }

    public string AccInfoServiceApi { get; set; }

    public Dictionary<string, AccInfoModel> AccInfos { get; set; } = [];
}