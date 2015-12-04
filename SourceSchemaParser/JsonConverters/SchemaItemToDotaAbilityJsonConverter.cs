﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaItemToDotaAbilityJsonConverter : JsonConverter
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

            List<DotaAbilitySchemaItem> abilities = new List<DotaAbilitySchemaItem>();

            JToken t = JToken.Load(reader);
            var properties = t.Children<JProperty>();
            foreach (var item in properties)
            {
                if(item.Name == "Version")
                {
                    continue;
                }

                JObject o = (JObject)item.Value;

                DotaAbilitySchemaItem abilitySchemaItem = JsonConvert.DeserializeObject<DotaAbilitySchemaItem>(item.Value.ToString());
                abilitySchemaItem.Name = item.Name;

                abilities.Add(abilitySchemaItem);
            }

            return abilities;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaAbilitySchemaItem>).IsAssignableFrom(objectType);
        }
    }
}