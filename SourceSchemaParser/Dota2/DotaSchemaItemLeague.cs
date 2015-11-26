using Newtonsoft.Json;

namespace SourceSchemaParser
{
    public class DotaSchemaItemLeague : SchemaItem
    {
        public DotaSchemaItemTool Tool { get; set; }

        [JsonProperty("tournament_url")]
        public string TournamentUrl { get; set; }
    }
}