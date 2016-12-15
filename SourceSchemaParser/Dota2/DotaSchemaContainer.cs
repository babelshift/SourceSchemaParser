using Newtonsoft.Json;

namespace SourceSchemaParser.DOTA2
{
    internal class DotaSchemaContainer
    {
        [JsonProperty("items_game")]
        public DotaSchema Schema { get; set; }
    }
}