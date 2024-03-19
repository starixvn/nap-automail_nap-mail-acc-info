using Fanex.MailDelivery.Core.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fanex.AccInfo.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController(IMainService service) : ControllerBase
{
    [HttpGet]
    public JobMessage Get()
    {
        return service.Execute(AppConfigService.Config.ScheduleTime);
    }
}