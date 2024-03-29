using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fanex.AccInfo.ServiceTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        [HttpGet]
        public JsonResult Execute()
        {
            AccInfoConfigService.Config.ApiUrl = "https://accinfo.nexdev.net/";
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