using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System.Collections.Generic;

namespace SourceSchemaParser.DOTA2
{
    internal class DotaItemBuildSchemaItem
    {
        public string Author { get; set; }
        public string Hero { get; set; }
        public string Title { get; set; }

        [JsonConverter(typeof(DotaItemBuildGroupSchemaItemJsonConverter))]
        public IList<DotaItemBuildGroupSchemaItem> Items { get; set; }
    }
}