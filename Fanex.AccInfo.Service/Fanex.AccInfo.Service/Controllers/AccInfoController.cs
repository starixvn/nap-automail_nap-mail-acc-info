using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.AccInfo.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class AccInfoController() : ControllerBase
{
    [HttpPost("Query")]
    public JsonResult Query(ParamModel param)
    {
        var result = new Dictionary<string, AccInfoModel>();
        if (!param.ApiKey.Equals(AppConfigService.Config.ApiKey))
        {
            return new JsonResult(result);
        }

        param.Keys.ForEach(key =>
        {
            var acc = AppConfigService.Config.AccInfos.FirstOrDefault(s => s.Key == key);
            if (acc.Key != null)
            {
                result.Add(acc.Key, acc.Value);
            }
        });

        return new JsonResult(result);
    }
}