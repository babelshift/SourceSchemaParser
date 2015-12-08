using Newtonsoft.Json;

namespace SourceSchemaParser.Dota2
{
    internal class DotaSchemaContainer
    {
        [JsonProperty("items_game")]
        public DotaSchema Schema { get; set; }
    }
}