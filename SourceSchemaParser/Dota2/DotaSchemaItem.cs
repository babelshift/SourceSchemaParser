using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.Dota2
{
    internal class DotaSchemaItem : SchemaItem
    {
        public DotaSchemaItemTool Tool { get; set; }

        [JsonProperty("tournament_url")]
        public string TournamentUrl { get; set; }

        [JsonProperty("image_banner")]
        public string ImageBannerPath { get; set; }

        [JsonProperty("item_rarity")]
        public string ItemRarity { get; set; }

        [JsonProperty("item_quality")]
        public string ItemQuality { get; set; }

        [JsonProperty("item_slot")]
        public string ItemSlot { get; set; }

        [JsonProperty("price_info")]
        public DotaSchemaItemPriceInfo PriceInfo { get; set; }

        [JsonConverter(typeof(DotaSchemaUsedByHeroesJsonConverter))]
        [JsonProperty("used_by_heroes")]
        public IList<string> UsedByHeroes { get; set; }

        [JsonConverter(typeof(DotaSchemaItemBundleJsonConverter))]
        [JsonProperty("bundle")]
        public IList<string> BundledItems { get; set; }
    }

    public class DotaSchemaItemPriceInfo
    {
        [JsonProperty("bucket")]
        public string Bucket { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("category_tags")]
        public string CategoryTags { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonConverter(typeof(DotaSchemaItemPriceJsonConverter))]
        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonConverter(typeof(StringToBoolJsonConverter))]
        [JsonProperty("is_pack_item")]
        public bool? IsPackItem { get; set; }
    }
}