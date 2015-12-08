using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;

namespace SourceSchemaParser.Dota2
{
    public class DotaSchemaItem : SchemaItem
    {
        public DotaSchemaItemTool Tool { get; set; }

        [JsonProperty("tournament_url")]
        public string TournamentUrl { get; set; }

        [JsonProperty("image_banner")]
        public string ImageBannerPath { get; set; }

        [JsonProperty("item_rarity")]
        public string ItemRarity { get; set; }

        //[JsonConverter(typeof(StringToBoolJsonConverter))]
        //[JsonProperty("used_by_heroes")]
        //public bool UsedByHeroes { get; set; }
    }
}