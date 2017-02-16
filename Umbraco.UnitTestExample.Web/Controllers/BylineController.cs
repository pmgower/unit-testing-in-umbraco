using System;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public class BylineController : SurfaceController
    {
        public BylineController(UmbracoContext umbracoContext, UmbracoHelper umbracoHelper)
            : base(umbracoContext, umbracoHelper)
        {
        }

        public PartialViewResult Byline()
        {
            var content = Umbraco.AssignedContentItem;
            return PartialView("Byline", new BylineModel(
                content.GetPropertyValue<string>("author").IfNullOrWhiteSpace(content.WriterName), 
                content.GetPropertyValue<DateTime?>("date") ?? content.CreateDate
            ));
        }
    }
}