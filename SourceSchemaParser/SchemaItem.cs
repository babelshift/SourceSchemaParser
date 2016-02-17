using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System;

namespace SourceSchemaParser
{
    public class SchemaItem
    {
        public string DefIndex { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_inventory")]
        public string ImageInventoryPath { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("item_description")]
        public string ItemDescription { get; set; }

        [JsonProperty("item_type_name")]
        public string ItemTypeName { get; set; }

        [JsonProperty("prefab")]
        public string Prefab { get; set; }

        [JsonConverter(typeof(DotaSchemaItemCreationDateJsonConverter))]
        [JsonProperty("creation_date")]
        public DateTime? CreationDate { get; set; }

        [JsonProperty("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
    }
}