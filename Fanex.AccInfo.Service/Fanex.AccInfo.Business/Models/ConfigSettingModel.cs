using System;
using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Models;

public class ConfigSettingModel
{
    public string SentryClientKey { get; set; }

    public string CategorySentryLog { get; set; }

    public string LetstalkServiceApi { get; set; }

    public string TargetName { get; set; }

    public ScheduleTime ScheduleTime { get; set; }

    public DateTime ExecTime4Gmt { get; set; }

    public List<ReportModel> Reports { get; set; }

    public string Path { get; set; }
}