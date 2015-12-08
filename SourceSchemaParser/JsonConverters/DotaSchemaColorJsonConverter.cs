using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaColorJsonConverter : JsonConverter
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

            List<DotaSchemaColor> colors = new List<DotaSchemaColor>();

            var colorProperties = t.Children<JProperty>();
            foreach (var colorProperty in colorProperties)
            {
                var color = colorProperty.Value.ToObject<DotaSchemaColor>();
                color.Name = colorProperty.Name;
                colors.Add(color);
            }

            return colors;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DotaSchemaColor).IsAssignableFrom(objectType);
        }
    }
}