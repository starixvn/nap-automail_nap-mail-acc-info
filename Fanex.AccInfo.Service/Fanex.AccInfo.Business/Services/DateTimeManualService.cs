using System;

namespace Fanex.AccInfo.Business.Services;

public class DateTimeManualService : IDateTimeService
{
    public DateTime ExecTime4Gmt
    {
        get
        {
            return AppConfigService.Config.ExecTime4Gmt;
        }
    }
}