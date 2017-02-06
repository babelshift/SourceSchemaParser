using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemBundleJsonConverter : JsonConverter
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

            List<string> bundledItems = new List<string>();

            var bundleProperties = t.Children<JProperty>();
            foreach (var bundleProperty in bundleProperties)
            {
                bundledItems.Add(bundleProperty.Name);
            }

            return bundledItems;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<string>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}