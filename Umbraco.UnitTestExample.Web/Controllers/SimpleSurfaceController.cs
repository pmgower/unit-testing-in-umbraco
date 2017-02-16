using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public class SimpleSurfaceController : SurfaceController
    {
        public SimpleSurfaceController(UmbracoContext umbracoContext) : base(umbracoContext)
        {
        }

        public SimpleSurfaceController(UmbracoContext umbracoContext, UmbracoHelper umbracoHelper) : base(umbracoContext, umbracoHelper)
        {
        }

        public SimpleSurfaceController()
        {
        }

        [ChildActionOnly]
        public ViewResult AddForm(AdditionModel model)
        {
            if (!model.IsPosted)
                return View("AddForm", model);

            model.Sum = model.X + model.Y;
            return View("AddResult", model);
        }

        public ActionResult PostForm()
        {
            return CurrentUmbracoPage();
        }
    }
}