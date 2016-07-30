using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemsJsonConverter : JsonConverter
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

            List<DotaSchemaItem> items = new List<DotaSchemaItem>();

            var itemProperties = t.Children<JProperty>();
            foreach (var itemProperty in itemProperties)
            {
                var item = itemProperty.Value.ToObject<DotaSchemaItem>();
                item.DefIndex = itemProperty.Name;
                items.Add(item);
            }

            return items;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaItem>).IsAssignableFrom(objectType);
        }
    }
}