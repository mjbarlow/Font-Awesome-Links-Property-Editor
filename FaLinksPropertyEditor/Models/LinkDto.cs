using Newtonsoft.Json;
using Umbraco.Core;

namespace FaLinksPropertyEditor.Models
{
    public partial class LinkDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("udi")]
        public GuidUdi Udi { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("queryString")]
        public string QueryString { get; set; }
    }
}
