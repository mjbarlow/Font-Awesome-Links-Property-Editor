using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PublishedCache;
using FaIcon = FaLinksPropertyEditor.Models.FaIcon;


namespace FaLinksPropertyEditor.PropertyValueConverters
{
    class FaIconPropertyValueConverter : PropertyValueConverterBase
    {
        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public override bool IsConverter(PublishedPropertyType propertyType) =>
            (propertyType.EditorAlias).Equals("FaIconPropertyEditor");

        /// <summary>
        /// Gets the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public override Type GetPropertyValueType(PublishedPropertyType propertyType)
        =>
            GetMaxNumber(propertyType.DataType.Configuration) == 1
                ? typeof(FaIcon)
                : typeof(IEnumerable<FaIcon>);
        

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType) =>
            PropertyCacheLevel.Snapshot;

        public override bool? IsValue(object value, PropertyValueLevel level) => value?.ToString() != "[]";

        public override object ConvertSourceToIntermediate(IPublishedElement owner, PublishedPropertyType propertyType,
            object source, bool preview) => source?.ToString();

        public override object ConvertIntermediateToObject(IPublishedElement owner, PublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            var maxNumber = GetMaxNumber(propertyType.DataType.Configuration);
            if (inter == null)
            {
                return maxNumber == 1 ? null : Enumerable.Empty<FaIcon>();
            }
            var faIcons = JsonConvert.DeserializeObject<IEnumerable<FaIcon>>(inter.ToString()).ToList();
   
            if (maxNumber == 1) return faIcons.FirstOrDefault();
            if (maxNumber > 0) return faIcons.Take(maxNumber);
            return faIcons;
        }

        private int GetMaxNumber(object configuration)
        {
            var json = JsonConvert.SerializeObject(configuration);
            var maxNumber = 0;
            var config = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            if(config.ContainsKey("maxNumber"))
            {
                maxNumber = config["maxNumber"];
            }
            return maxNumber;
        }
    }
}
