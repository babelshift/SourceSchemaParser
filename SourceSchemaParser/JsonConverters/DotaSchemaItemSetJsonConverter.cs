using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemSetJsonConverter : JsonConverter
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

            JToken t = JToken.Load(reader);

            List<DotaSchemaItemSet> itemSets = new List<DotaSchemaItemSet>();

            var setProperties = t.Children<JProperty>();
            foreach (var setProperty in setProperties)
            {
                var itemSet = setProperty.Value.ToObject<DotaSchemaItemSet>();
                itemSet.RawName = setProperty.Name;
                itemSets.Add(itemSet);
            }

            return itemSets;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaItemSet>).IsAssignableFrom(objectType);
        }
    }
}