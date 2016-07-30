using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System.Collections.Generic;

namespace SourceSchemaParser.Dota2
{
    internal class DotaSchemaItemSet
    {
        public string RawName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("store_bundle")]
        public string StoreBundleName { get; set; }

        [JsonConverter(typeof(DotaSchemaItemSetItemsJsonConverter))]
        [JsonProperty("items")]
        public IList<string> Items { get; set; }
    }
}