using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web;
using FaLinksPropertyEditor.PropertyValueConverters;

namespace FaLinksPropertyEditor.Web.Controllers
{
    public class TestController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            // Do some stuff here, the return the base Index method
            var test = model.Content.Value<string>("testProperty");
            var test1 = model.Content.Value<IEnumerable<FaLink>>("testProperty");

            return base.Index(model);
        }
    }
}