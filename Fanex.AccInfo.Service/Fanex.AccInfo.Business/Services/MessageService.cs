using Fanex.AccInfo.Business.Models;

namespace Fanex.AccInfo.Business.Services;

public interface IMessageService
{
    LetstalkMessageModel GetMessageModel(ReportModel reportModel, ScheduleTime scheduleTime);
}