using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
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
