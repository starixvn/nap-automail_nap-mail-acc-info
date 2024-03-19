using Fanex.MailDelivery.Client.Jobs;
using Fanex.MailDelivery.Core.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Fanex.AccInfo.Service.Services;

public class ExecuteService : JobBase
{
    protected override JobMessage Run(Schedule schedule, IJobExecutionContext jobContext)
    {
        var services = ServiceCollectionExtensions.Services;
        var serviceProvider = services.BuildServiceProvider();

        var mainService = serviceProvider.GetService<IMainService>();
        var result = mainService!.Execute(AppConfigService.Config.ScheduleTime);
        return result;
    }
}