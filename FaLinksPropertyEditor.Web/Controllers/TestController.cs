using FaLinksPropertyEditor.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace FaLinksPropertyEditor.Web.Controllers
{
    public class TestController : Umbraco.Web.Mvc.RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            // Do some stuff here, the return the base Index method
            var iconSingle = model.Content.Value<FaIcon>("iconSingle");
            var iconMultiple = model.Content.Value<IEnumerable<FaIcon>>("iconMultiple");

            var linkSingle = model.Content.Value<FaLink>("linkSingle");
            var linkMultiple = model.Content.Value<IEnumerable<FaLink>>("linkMultiple");



            return base.Index(model);
        }
    }
}