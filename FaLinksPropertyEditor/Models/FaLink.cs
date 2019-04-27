using System.Collections.Generic;
using System.Linq;
using Umbraco.Web.Models;

namespace FaLinksPropertyEditor.Models
{
    public partial class FaLink
    {
        public string PrimaryClass { get; set; }
        public string SecondaryClass { get; set; }
        public string ClassName { get; set; }
        public string Svg { get; set; }
        public string Label { get; set; }
        public Link Link { get; set; }

        public FaLink(FaLinkDto faLinkDto, List<Link> links)
        {
            PrimaryClass = faLinkDto.PrimaryClass;
            SecondaryClass = faLinkDto.SecondaryClass;
            ClassName = faLinkDto.ClassName;
            Svg = faLinkDto.Svg;
            Label = faLinkDto.Label;
            Link = links.FirstOrDefault();
        }
    }
}
