using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public class DotaSchemaItemAutograph
    {
        public string DefIndex { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("autograph")]
        public string Autograph { get; set; }
        [JsonProperty("workshoplink")]
        public long? WorkshopLink { get; set; }
        [JsonProperty("language")]
        public int Language { get; set; }
        [JsonProperty("icon_path")]
        public string IconPath { get; set; }
        [JsonProperty("name_modifier")]
        public string Modifier { get; set; }
    }
}
