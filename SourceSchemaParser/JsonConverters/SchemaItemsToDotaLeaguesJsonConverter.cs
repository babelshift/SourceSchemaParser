using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaItemsToDotaLeaguesJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            List<DotaSchemaItem> leagues = new List<DotaSchemaItem>();

            JToken t = JToken.Load(reader);
            var properties = t.Children<JProperty>();
            foreach (var item in properties)
            {
                JObject o = (JObject)item.Value;

                bool isLeague = o["prefab"] != null && o["prefab"].ToString() == "league";

                bool isAdmin =
                    o["tool"] != null
                    && o["tool"]["usage"] != null
                    && o["tool"]["usage"]["admin"] != null
                    && o["tool"]["usage"]["admin"].ToString() == "1";

                if (isLeague && !isAdmin)
                {
                    var league = JsonConvert.DeserializeObject<DotaSchemaItem>(item.Value.ToString());
                    league.DefIndex = item.Name;
                    leagues.Add(league);
                }
            }

            return leagues;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaItem>).IsAssignableFrom(objectType);
        }
    }
}