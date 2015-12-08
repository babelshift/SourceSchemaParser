using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System.Collections.Generic;

namespace SourceSchemaParser.Dota2
{
    public class DotaSchema
    {
        [JsonProperty("game_info")]
        public DotaSchemaGameInfo GameInfo { get; set; }

        [JsonConverter(typeof(DotaSchemaRarityJsonConverter))]
        [JsonProperty("rarities")]
        public IList<DotaSchemaRarity> Rarities { get; set; }

        [JsonConverter(typeof(DotaSchemaQualityJsonConverter))]
        [JsonProperty("qualities")]
        public IList<DotaSchemaQuality> Qualities { get; set; }

        [JsonConverter(typeof(DotaSchemaColorJsonConverter))]
        [JsonProperty("colors")]
        public IList<DotaSchemaColor> Colors { get; set; }

        [JsonConverter(typeof(DotaSchemaItemsJsonConverter))]
        [JsonProperty("items")]
        public IList<DotaSchemaItem> Items { get; set; }
    }

    public class DotaSchemaColor
    {
        public string Name { get; set; }

        [JsonProperty("color_name")]
        public string ColorName { get; set; }

        [JsonProperty("hex_color")]
        public string HexColor { get; set; }
    }

    public class DotaSchemaQuality
    {
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("hexColor")]
        public string HexColor { get; set; }
    }

    public class DotaSchemaRarity
    {
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("loc_key")]
        public string LocKey { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("next_rarity")]
        public string NextRarity { get; set; }
    }

    public class DotaSchemaGameInfo
    {
        [JsonProperty("first_valid_class")]
        public string FirstValidClass { get; set; }

        [JsonProperty("last_valid_class")]
        public string LastValidClass { get; set; }

        [JsonProperty("first_valid_item_slot")]
        public string FirstValidItemSlot { get; set; }

        [JsonProperty("last_valid_item_slot")]
        public string LastValidItemSlot { get; set; }

        [JsonProperty("num_item_presets")]
        public string ItemPresetCount { get; set; }
    }
}