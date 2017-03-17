using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public class SimpleApiController : ApiControllerBase
    {
        public SimpleApiController(UmbracoContext umbracoContext, UmbracoHelper umbracoHelper) : base(umbracoContext, umbracoHelper)
        {
        }

        public SimpleApiController()
        {
        }

        // /umbraco/api/poi/info/{id}
        [HttpGet]
        public IHttpActionResult Subtract(SubtrationModel model)
        {
            IHttpActionResult result = this.BadRequest();
            try
            {
                if (model.X == -1 && model.Y == -1)
                {
                    throw new Exception("Error with -1 as X & Y");
                }
                else
                {
                    var jsonSettings = new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };
                    string messageAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { statusCode = 200, message = "OK", result = model.Result }, Newtonsoft.Json.Formatting.None, jsonSettings);
                    result = this.JsonString(messageAsJson);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = exception.Message;
                Logger.Error(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, errorMessage, exception);

                var statusCode = HttpStatusCode.InternalServerError;
                string messageAsJson = Newtonsoft.Json.JsonConvert.SerializeObject(new { statusCode = statusCode, message = errorMessage });
                result = this.JsonString(messageAsJson, statusCode);
            }

            return result;
        }

    }
}