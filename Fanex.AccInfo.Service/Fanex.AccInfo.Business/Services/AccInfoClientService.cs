using Fanex.AccInfo.Business.Models;
using Fanex.WebApiClient.Utility;
using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Services
{
    public interface AccInfoClientService
    {
        Dictionary<string, AccInfoModel> Query(ParamModel param);
    }

    public class AccInfoClientServiceImp : AccInfoClientService
    {
        public AccInfoClientServiceImp()
        {
            WebApiClientService.ApiUrl = AccInfoConfigService.Config.ApiUrl;
        }

        public Dictionary<string, AccInfoModel> Query(ParamModel param)
        {
            const string action = "AccInfo/Query";
            return WebApiClientService.PostAsync<Dictionary<string, AccInfoModel>, ParamModel>(action, param);
        }
    }
}