using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;

namespace FaLinksPropertyEditor.Configuration
{
    public partial class FaLinksConfiguration
    {
        [ConfigurationField("minNumber", "Minimum number of items", "number")]
        public int MinNumber { get; set; }

        [ConfigurationField("maxNumber", "Maximum number of items", "number")]
        public int MaxNumber { get; set; }
    }
}
