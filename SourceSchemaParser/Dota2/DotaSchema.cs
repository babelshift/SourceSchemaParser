using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System.Collections.Generic;

namespace SourceSchemaParser.DOTA2
{
    internal class DotaSchema
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

        [JsonConverter(typeof(DotaSchemaPrefabJsonConverter))]
        [JsonProperty("prefabs")]
        public IList<DotaSchemaPrefab> Prefabs { get; set; }

        [JsonConverter(typeof(DotaSchemaItemsJsonConverter))]
        [JsonProperty("items")]
        public IList<DotaSchemaItem> Items { get; set; }

        [JsonConverter(typeof(DotaSchemaItemSetJsonConverter))]
        [JsonProperty("item_sets")]
        public IList<DotaSchemaItemSet> ItemSets { get; set; }

        [JsonConverter(typeof(DotaSchemaItemAutographsJsonConverter))]
        [JsonProperty("items_autographs")]
        public IList<DotaSchemaItemAutograph> ItemAutographs { get; set; }
    }

    public class DotaSchemaPrefabCapability
    {
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("nameable")]
        public bool Nameable { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("can_have_sockets")]
        public bool CanHaveSockets { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("gems_can_be_extracted")]
        public bool GemsCanBeExtracted { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("can_gift_wrap")]
        public bool CanGiftWrap { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("usable_gc")]
        public bool UsableGC { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("usable_out_of_game")]
        public bool UsableOutOfGame { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("decodable")]
        public bool Decodable { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("usable")]
        public bool Usable { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("is_gem")]
        public bool IsGem { get; set; }
    }

    public class DotaSchemaPrefab
    {
        public string Type { get; set; }

        [JsonProperty("item_type_name")]
        public string TypeName { get; set; }

        [JsonProperty("item_class")]
        public string Class { get; set; }

        [JsonProperty("item_name")]
        public string Name { get; set; }

        [JsonProperty("item_slot")]
        public string Slot { get; set; }

        [JsonProperty("item_quality")]
        public string Quality { get; set; }

        [JsonProperty("item_rarity")]
        public string Rarity { get; set; }

        [JsonProperty("min_ilevel")]
        public string MinItemLevel { get; set; }

        [JsonProperty("max_ilevel")]
        public string MaxItemLevel { get; set; }

        [JsonProperty("image_inventory_size_w")]
        public string ImageInventorySizeWidth { get; set; }

        [JsonProperty("image_inventory_size_h")]
        public string ImageInventorySizeHeight { get; set; }

        [JsonProperty("capabilities")]
        public DotaSchemaPrefabCapability Capabilities { get; set; }
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