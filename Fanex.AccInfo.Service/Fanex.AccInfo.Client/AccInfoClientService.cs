using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;

namespace Fanex.AccInfo.Client
{
    public interface AccInfoClientService
    {
        Dictionary<string, AccInfoModel> Query(ParamModel param);
    }

    public class AccInfoClientServiceImp : AccInfoClientService
    {
        public AccInfoClientServiceImp()
        {
            WebApiClientService.AccInfoServiceApi = AppConfigService.Config.AccInfoServiceApi;
        }

        public Dictionary<string, AccInfoModel> Query(ParamModel param)
        {
            const string action = "AccInfo/Query";
            return WebApiClientService.PostAsync<Dictionary<string, AccInfoModel>, ParamModel>(action, param);
        }
    }
}