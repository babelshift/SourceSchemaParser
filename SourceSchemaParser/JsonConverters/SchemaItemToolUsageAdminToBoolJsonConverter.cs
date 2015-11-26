using Newtonsoft.Json;
using System;

namespace SourceSchemaParser.JsonConverters
{
    internal class SchemaItemToolUsageAdminToBoolJsonConverter : JsonConverter
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

            if (reader.Value != null)
            {
                string value = reader.Value.ToString();
                if (value == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(String).IsAssignableFrom(objectType);
        }
    }
}