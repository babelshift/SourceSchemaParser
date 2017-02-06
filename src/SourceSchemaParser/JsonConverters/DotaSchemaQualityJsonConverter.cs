using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaQualityJsonConverter : JsonConverter
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

            List<DotaSchemaQuality> qualities = new List<DotaSchemaQuality>();

            var qualityProperties = t.Children<JProperty>();
            foreach (var qualityProperty in qualityProperties)
            {
                var quality = qualityProperty.Value.ToObject<DotaSchemaQuality>();
                quality.Name = qualityProperty.Name;
                qualities.Add(quality);
            }

            return qualities;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DotaSchemaQuality).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}