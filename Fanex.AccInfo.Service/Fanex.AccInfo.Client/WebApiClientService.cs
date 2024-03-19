using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Fanex.AccInfo.Client
{
    public static class WebApiClientService
    {
        private static HttpClient client = new();
        private static readonly MediaTypeWithQualityHeaderValue Media = new("application/json");
        public static string? AccInfoServiceApi { get; set; }

        public static T PostAsync<T, P>(string action, P inputParams)
        {
            var rootUrl = NormalizeBaseUrl(AccInfoServiceApi);
            client = new HttpClient();
            client.BaseAddress = new Uri(rootUrl);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(Media);

            using var content = new StringContent(JsonConvert.SerializeObject(inputParams), Encoding.UTF8, "application/json");
            var responseResult = client.PostAsync(action, content).Result;
            if (responseResult.IsSuccessStatusCode)
            {
                var data = responseResult.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data.Result)!;
            }

            client.Dispose();

            return default!;
        }

        public static T PostAsync<T, P>(string url, string action, P inputParams)
        {
            client = new HttpClient();
            var rootUrl = NormalizeBaseUrl(url);
            client.BaseAddress = new Uri(rootUrl);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(Media);

            using var content = new StringContent(JsonConvert.SerializeObject(inputParams), Encoding.UTF8, "application/json");
            var responseResult = client.PostAsync(action, content).Result;
            if (responseResult.IsSuccessStatusCode)
            {
                var data = responseResult.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data.Result)!;
            }

            client.Dispose();

            return default!;
        }

        private static string NormalizeBaseUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("AccServiceApi Url can't not be null.");
            }

            return url.EndsWith("/") ? url : url + "/";
        }
    }
}