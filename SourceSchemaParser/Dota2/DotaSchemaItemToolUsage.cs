using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;

namespace SourceSchemaParser.Dota2
{
    internal class DotaSchemaItemToolUsage
    {
        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        public string Tier { get; set; }
        public string Location { get; set; }

        [JsonConverter(typeof(SchemaItemToolUsageAdminToBoolJsonConverter))]
        public bool Admin { get; set; }
    }
}