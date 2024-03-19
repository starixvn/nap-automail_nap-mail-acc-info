using Fanex.AccInfo.Business.Models;
using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Services;

public interface IDateTimeProviderService
{
    IDateTimeService DateTime(ScheduleTime scheduleTime);
}

public class DateTimeProviderServiceImp : IDateTimeProviderService
{
    public IDateTimeService DateTime(ScheduleTime scheduleTime)
    {
        var datetime = new Dictionary<ScheduleTime, IDateTimeService>
        {
            { ScheduleTime.Manual, new DateTimeManualService() },
            { ScheduleTime.Hourly, new DateTimeAutoService() },
            { ScheduleTime.Daily, new DateTimeDailyService() }
        };

        return datetime[scheduleTime];
    }
}