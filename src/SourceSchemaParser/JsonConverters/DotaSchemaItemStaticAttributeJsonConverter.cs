using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using SourceSchemaParser.DOTA2;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemStaticAttributeJsonConverter : JsonConverter
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

            List<DotaSchemaItemStaticAttribute> attributes = new List<DotaSchemaItemStaticAttribute>();

            var attributeProperties = t.Children<JProperty>();
            foreach (var attributesProperty in attributeProperties)
            {
                var attribute = attributesProperty.Value.ToObject<DotaSchemaItemStaticAttribute>();
                attributes.Add(attribute);
            }

            return attributes;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaItemStaticAttribute>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}