using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.JsonConverters
{
    public class DotaSchemaItemCreationDateJsonConverter : JsonConverter
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

            // we have to check for this because the schema format is screwed up for Devourling where the date is actually an object
            if(reader.TokenType == JsonToken.StartObject)
            {
                return null;
            }

            JValue token = (JValue)JToken.Load(reader);

            DateTime creationDate = DateTime.Now;
            bool success = DateTime.TryParse(token.Value.ToString(), out creationDate);
            if (success)
            {
                return creationDate;
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
