using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    /// <summary>
    /// Extension methond recommended by Nkosi on 11/26/2016 on stackoverflow:
    /// http://stackoverflow.com/a/40836422
    /// </summary>
    public static class JsonStringResultExtension
    {
        /// <summary>
        /// Defaults to 200 OK status code if none is specified.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="jsonContent"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static CustomJsonStringResult JsonString(this UmbracoApiController controller, string jsonContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var result = new CustomJsonStringResult(controller.Request, statusCode, jsonContent);
            return result;
        }

        public class CustomJsonStringResult : IHttpActionResult
        {
            public string Json { get; }
            private HttpStatusCode statusCode;
            private HttpRequestMessage request;

            public CustomJsonStringResult(HttpRequestMessage httpRequestMessage, HttpStatusCode statusCode = HttpStatusCode.OK, string json = "")
            {
                this.request = httpRequestMessage;
                this.Json = json;
                this.statusCode = statusCode;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(Execute());
            }

            private HttpResponseMessage Execute()
            {
                var response = request.CreateResponse(statusCode);
                response.Content = new StringContent(this.Json, Encoding.UTF8, "application/json");
                return response;
            }
        }
    }
}