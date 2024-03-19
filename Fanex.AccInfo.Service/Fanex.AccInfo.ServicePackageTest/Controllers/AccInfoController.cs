using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Fanex.AccInfo.Client;
using Microsoft.AspNetCore.Mvc;

namespace Fanex.AccInfo.ServicePackageTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccInfoController : ControllerBase
    {
        [HttpGet]
        public JsonResult Execute()
        {
            AppConfigService.Config.AccInfoServiceApi = "https://accinfo.nexdev.net/";
            var service = new AccInfoClientServiceImp();
            var param = new ParamModel
            {
                ApiKey = "3IlQmYfn@;(juVHrrf3oWnEv'JVCqWUiyfuk{V$;L^eh^36;5J",
                Keys = ["DBWH.bodb_dwrs.dwrs", "DBMMS.bodb_MMS.wsv_nap"]
            };

            var result = service.Query(param);

            return new JsonResult(result);
        }
    }
}