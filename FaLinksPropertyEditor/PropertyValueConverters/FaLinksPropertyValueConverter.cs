using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.Models;
using Umbraco.Web.PropertyEditors;
using System.Runtime.Serialization;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Editors;
using Umbraco.Core.Models.Entities;
using Umbraco.Core.Services;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.PublishedCache;


namespace FaLinksPropertyEditor.PropertyValueConverters
{
    class FaLinksPropertyValueConverter : PropertyValueConverterBase
    {

        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;


        public FaLinksPropertyValueConverter(IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _publishedSnapshotAccessor = publishedSnapshotAccessor ?? throw new ArgumentNullException(nameof(publishedSnapshotAccessor));
        }
        
        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public override bool IsConverter(PublishedPropertyType propertyType) =>
            (propertyType.EditorAlias).Equals("FaLinksPropertyEditor");


        /// <summary>
        /// Gets the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public override Type GetPropertyValueType(PublishedPropertyType propertyType) => typeof(IEnumerable<FaLink>);

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType) =>
            PropertyCacheLevel.Snapshot;

        public override bool? IsValue(object value, PropertyValueLevel level) => value?.ToString() != "[]";

        public override object ConvertSourceToIntermediate(IPublishedElement owner, PublishedPropertyType propertyType,
            object source, bool preview) => source?.ToString();

        public override object ConvertIntermediateToObject(IPublishedElement owner, PublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
        {
            //todo: probably a better way of doing this
            var faLinkDtos = JsonConvert.DeserializeObject<IEnumerable<FaLinkDto>>(inter.ToString()).ToList();
            var faLinks = new List<FaLink>();
            foreach (var faLinkDto in faLinkDtos)
            {
                var links = new List<Link>();
                foreach (var dto in faLinkDto.Link)
                {
                    var type = LinkType.External;
                    var url = dto.Url;

                    if (dto.Udi != null)
                    {
                        type = dto.Udi.EntityType == Umbraco.Core.Constants.UdiEntityType.Media
                            ? LinkType.Media
                            : LinkType.Content;

                        var content = type == LinkType.Media
                            ? _publishedSnapshotAccessor.PublishedSnapshot.Media.GetById(preview, dto.Udi.Guid)
                            : _publishedSnapshotAccessor.PublishedSnapshot.Content.GetById(preview, dto.Udi.Guid);

                        if (content == null || content.ItemType == PublishedItemType.Element)
                        {
                            continue;
                        }

                        url = content.Url;
                    }

                    links.Add(
                        new Link
                        {
                            Name = dto.Name,
                            Target = dto.Target,
                            Type = type,
                            Udi = dto.Udi,
                            Url = url + dto.QueryString,
                        }
                    );
                }
                faLinks.Add(new FaLink(faLinkDto,links));
            }
            return faLinks;
        }
    }

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
