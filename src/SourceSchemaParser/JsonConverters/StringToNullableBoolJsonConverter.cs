using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    public class StringToNullableBoolJsonConverter : JsonConverter
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

            JValue v = (JValue)JToken.Load(reader);
            if (v.Value.ToString() == "0")
            {
                return false;
            }
            else if (v.Value.ToString() == "1")
            {
                return true;
            }
            else
            {
                return null;
            }
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(string).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}