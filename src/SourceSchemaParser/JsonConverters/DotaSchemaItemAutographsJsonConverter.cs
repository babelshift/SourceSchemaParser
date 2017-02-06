using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaItemAutographsJsonConverter : JsonConverter
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

            List<DotaSchemaItemAutograph> items = new List<DotaSchemaItemAutograph>();

            var autographProperties = t.Children<JProperty>();
            foreach (var autographProperty in autographProperties)
            {
                var autograph = autographProperty.Value.ToObject<DotaSchemaItemAutograph>();
                autograph.DefIndex = autographProperty.Name;
                items.Add(autograph);
            }

            return items;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaItemAutograph>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}