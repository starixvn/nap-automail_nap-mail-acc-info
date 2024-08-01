using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Fanex.AccInfo.ServiceTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        [HttpGet]
        public JsonResult Execute()
        {
            AccInfoConfigService.Config.ApiUrl = "http://localhost:31599/";//"https://accinfo.nexdev.net/";

            var param = new ParamModel
            {
                ApiKey = "%MPAkBS8eZGkNEHD4M7fs6EQNPLrzJg98$",
                Keys = ["DBWH.bodb_dwrs.dwrs", "DBMMS.bodb_MMS.wsv_nap"]
            };

            var client = new HttpClient();
            client.BaseAddress = new Uri($"{AccInfoConfigService.Config.ApiUrl}Auth/Query");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(param, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var message = client.PostAsync(client.BaseAddress, content).Result;
            if (message.IsSuccessStatusCode)
            {
                var rs = message.Content.ReadAsStringAsync().Result;
            }

            return new JsonResult(token);
        }
    }
}