using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaLanguageTokensJsonConverter : JsonConverter
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

            Dictionary<string, string> tokens = new Dictionary<string, string>();

            JToken t = JToken.Load(reader);
            var tokenProperties = t.Children<JProperty>();
            foreach (var tokenProperty in tokenProperties)
            {
                tokens.Add(tokenProperty.Name, tokenProperty.Value.ToString());
            }

            return tokens;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IDictionary<string, string>).IsAssignableFrom(objectType);
        }
    }
}