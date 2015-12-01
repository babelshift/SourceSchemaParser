using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemHeroItemToDotaHeroJsonConverter : JsonConverter
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

            List<DotaHeroSchemaItem> heroes = new List<DotaHeroSchemaItem>();

            JToken t = JToken.Load(reader);
            var properties = t.Children<JProperty>();
            foreach (var item in properties)
            {
                if(item.Name == "Version")
                {
                    continue;
                }

                JObject o = (JObject)item.Value;

                DotaHeroSchemaItem heroSchemaItem = JsonConvert.DeserializeObject<DotaHeroSchemaItem>(item.Value.ToString());
                heroSchemaItem.Name = item.Name;

                heroes.Add(heroSchemaItem);
            }

            return heroes;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaHeroSchemaItem>).IsAssignableFrom(objectType);
        }
    }
}
