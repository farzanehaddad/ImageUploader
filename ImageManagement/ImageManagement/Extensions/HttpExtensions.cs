using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GM.Ardvark.Search.Api.Extentions.Http
{
    public static class HttpExtension
    {
        public static async Task<HttpResponse> GenerateResponse(this HttpResponse response, int statusCode, object data)
        {
            if (!response.HasStarted)
            {
                var setting = new JsonSerializerSettings();
                setting.NullValueHandling = NullValueHandling.Ignore;
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                setting.MaxDepth = 5;
                var serialized = JsonConvert.SerializeObject(data, setting);
                response.ContentType = "application/json";
                response.StatusCode = statusCode;
                var body = Encoding.UTF8.GetBytes(serialized);
                await response.Body.WriteAsync(body, 0, body.Length);
            }

            return response;
        }

        public static async Task<HttpResponse> GenerateResponse(this HttpResponse response, int statusCode, string jsonStringData)
        {
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = statusCode;
                var body = Encoding.UTF8.GetBytes(jsonStringData);
                await response.Body.WriteAsync(body, 0, body.Length);
            }

            return response;
        }

        public static async Task<HttpResponse> GenerateResponse(this HttpResponse response, int statusCode, Stream content)
        {
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = statusCode;
                await content.CopyToAsync(response.Body);
            }

            return response;
        }
    }
}