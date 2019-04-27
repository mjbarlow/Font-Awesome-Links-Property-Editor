using Newtonsoft.Json;

namespace FaLinksPropertyEditor.Models
{
    public partial class FaLinkDto
    {
        [JsonProperty("primaryClass")]
        public string PrimaryClass { get; set; }

        [JsonProperty("secondaryClass")]
        public string SecondaryClass { get; set; }

        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("svg")]
        public string Svg { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("link")]
        public LinkDto[] Link { get; set; }
    }
}
