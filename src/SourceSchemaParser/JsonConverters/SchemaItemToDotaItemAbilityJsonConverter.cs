using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaItemToDotaItemAbilityJsonConverter : JsonConverter
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

            List<DotaItemAbilitySchemaItem> abilities = new List<DotaItemAbilitySchemaItem>();

            JToken t = JToken.Load(reader);
            var properties = t.Children<JProperty>();
            foreach (var item in properties)
            {
                if (item.Name == "Version")
                {
                    continue;
                }

                JObject o = (JObject)item.Value;

                DotaItemAbilitySchemaItem abilitySchemaItem = JsonConvert.DeserializeObject<DotaItemAbilitySchemaItem>(item.Value.ToString());
                abilitySchemaItem.Name = item.Name;

                abilities.Add(abilitySchemaItem);
            }

            return abilities;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaItemAbilitySchemaItem>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}