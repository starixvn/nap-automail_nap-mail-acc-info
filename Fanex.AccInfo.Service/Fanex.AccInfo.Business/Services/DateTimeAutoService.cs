using System;

namespace Fanex.AccInfo.Business.Services;

public class DateTimeAutoService : IDateTimeService
{
    public DateTime ExecTime4Gmt
    {
        get
        {
            var date = $"{DateTime.Now:yyyy-MM-dd HH:15:00}";
            return Convert.ToDateTime(date);
        }
    }
}