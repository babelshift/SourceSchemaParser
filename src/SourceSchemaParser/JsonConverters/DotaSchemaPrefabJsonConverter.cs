using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaPrefabJsonConverter : JsonConverter
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

            List<DotaSchemaPrefab> prefabs = new List<DotaSchemaPrefab>();

            var prefabProperties = t.Children<JProperty>();
            foreach (var prefabProperty in prefabProperties)
            {
                var prefab = prefabProperty.Value.ToObject<DotaSchemaPrefab>();
                prefab.Type = prefabProperty.Name;
                prefabs.Add(prefab);
            }

            return prefabs;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaSchemaPrefab>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}