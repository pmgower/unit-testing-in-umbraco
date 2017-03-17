using System.Linq;
using Umbraco.Web;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public abstract class ApiControllerBase : Umbraco.Web.WebApi.UmbracoApiController
    {
        protected ApiControllerBase()
        {
            //do nothing
        }

        protected ApiControllerBase(UmbracoContext umbracoContext, UmbracoHelper umbracoHelper) : base(umbracoContext, umbracoHelper)
        {
            //do nothing
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var formatters = controllerContext.Configuration.Formatters;
            var toRemove = formatters.Where(c => (c is System.Net.Http.Formatting.XmlMediaTypeFormatter)).ToList();
            foreach (var r in toRemove)
            {
                formatters.Remove(r);
            }
            formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}