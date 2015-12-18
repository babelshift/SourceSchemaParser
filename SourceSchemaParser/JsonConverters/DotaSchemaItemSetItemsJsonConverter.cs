using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemSetItemsJsonConverter : JsonConverter
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

            List<string> items = new List<string>();

            var itemsProperties = t.Children<JProperty>();
            foreach (var itemsProperty in itemsProperties)
            {
                items.Add(itemsProperty.Name);
            }

            return items;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<string>).IsAssignableFrom(objectType);
        }
    }
}