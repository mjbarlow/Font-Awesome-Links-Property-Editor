using Newtonsoft.Json;

namespace FaLinksPropertyEditor.Models
{
    public partial class FaIcon
    {
        [JsonProperty("className")]
        public string ClassName { get; set; }

        [JsonProperty("svg")]
        public string Svg { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
