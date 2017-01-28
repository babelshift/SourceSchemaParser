using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SourceSchemaParser.JsonConverters
{
    /// <summary>
    /// Had to add a JsonConverter specifically for DateTimes to handle DateTimes that aren't valid. Valve doesn't do checks on their dates apparently since I've seen schemas with June 31 in them.
    /// </summary>
    public class DateTimeJsonConverter : JsonConverter
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

            JValue token = (JValue)JToken.Load(reader);

            DateTime dateTime = DateTime.Now;
            bool success = DateTime.TryParse(token.Value.ToString(), out dateTime);
            if (success)
            {
                return dateTime;
            }
            else
            {
                return null;
            }
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DateTime).IsAssignableFrom(objectType);
        }
    }
}