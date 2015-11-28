using Newtonsoft.Json;

namespace SourceSchemaParser
{
    public class SchemaItem
    {
        public int DefIndex { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_inventory")]
        public string ImageInventoryPath { get; set; }

        [JsonProperty("item_name")]
        public string NameLocalized { get; set; }

        [JsonProperty("item_description")]
        public string DescriptionLocalized { get; set; }

        [JsonProperty("item_type_name")]
        public string TypeName { get; set; }
    }
}