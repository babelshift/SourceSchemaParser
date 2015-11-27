using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    internal class VRootTokenJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = value as VToken;
            if (token != null)
            {
                if (token.TokenType == VTokenType.Root)
                {
                    writer.WriteStartObject();
                    var root = token as VRootToken;
                    var collection = root.KeyValuePairs;
                    SerializeCollection(writer, serializer, collection);
                }

                if (token.TokenType == VTokenType.KeyValueCollection)
                {
                    var collection = token as VKeyValueCollection;
                    SerializeCollection(writer, serializer, collection);
                }
                else if (token.TokenType == VTokenType.KeyValuePair)
                {
                    var keyValue = token as VKeyValuePair;
                    writer.WritePropertyName(keyValue.Key);
                    writer.WriteValue(keyValue.Value);
                }

                if (token.TokenType == VTokenType.Root)
                {
                    writer.WriteEndObject();
                }
            }
        }

        private static void SerializeCollection(JsonWriter writer, JsonSerializer serializer, VKeyValueCollection collection)
        {
            writer.WritePropertyName(collection.Key);
            writer.WriteStartObject();
            foreach (var keyValue in collection.KeyValuePairs)
            {
                serializer.Serialize(writer, keyValue);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<VToken>).IsAssignableFrom(objectType);
        }
    }
}
