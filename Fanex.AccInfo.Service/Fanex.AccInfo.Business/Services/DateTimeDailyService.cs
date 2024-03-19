using System;

namespace Fanex.AccInfo.Business.Services;

public class DateTimeDailyService : IDateTimeService
{
    public DateTime ExecTime4Gmt
    {
        get
        {
            var date = $"{DateTime.Now:yyyy-MM-dd}";
            return Convert.ToDateTime(date);
        }
    }
}