using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using System.Collections.Generic;

namespace Fanex.AccInfo.Service461Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AppConfigService.Config.ApiUrl = "https://accinfo.nexdev.net/";
            var service = new AccInfoClientServiceImp();
            var param = new ParamModel
            {
                ApiKey = "3IlQmYfn@;(juVHrrf3oWnEv'JVCqWUiyfuk{V$;L^eh^36;5J",
                Keys = new List<string> { "DBWH.bodb_dwrs.dwrs", "DBMMS.bodb_MMS.wsv_nap" }
            };

            var result = service.Query(param);
        }
    }
}