using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SourceSchemaParser.JsonConverters
{
    public class DotaSchemaItemPriceJsonConverter : JsonConverter
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
            string price = v.Value.ToString();
            if (price != "0" && price.Length >= 2)
            {
                price = price.Insert(price.Length - 2, ".");
            }
            decimal result = 0m;
            bool success = decimal.TryParse(price, out result);
            return result;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(string).IsAssignableFrom(objectType);
        }
    }
}