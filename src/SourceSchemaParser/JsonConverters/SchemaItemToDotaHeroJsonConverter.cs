using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaItemToDotaHeroJsonConverter : JsonConverter
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
                if (item.Name == "Version")
                {
                    continue;
                }

                JObject o = (JObject)item.Value;

                DotaHeroSchemaItem heroSchemaItem = JsonConvert.DeserializeObject<DotaHeroSchemaItem>(item.Value.ToString());
                heroSchemaItem.Name = item.Name;
                if (heroSchemaItem.Team == "good") { heroSchemaItem.Team = "Good"; } // fix stupid caps

                heroes.Add(heroSchemaItem);
            }

            return heroes;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaHeroSchemaItem>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}