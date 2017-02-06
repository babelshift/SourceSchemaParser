using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaRarityJsonConverter : JsonConverter
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

            List<DotaSchemaRarity> rarities = new List<DotaSchemaRarity>();

            var rarityProperties = t.Children<JProperty>();
            foreach (var rarityProperty in rarityProperties)
            {
                var rarity = rarityProperty.Value.ToObject<DotaSchemaRarity>();
                rarity.Name = rarityProperty.Name;
                rarities.Add(rarity);

                //var itemProperties = rarityProperty.Value.Children<JProperty>();
                //foreach (var itemProperty in itemProperties)
                //{
                //    if (itemProperty.Name == "value") { rarity.Value = int.Parse(itemProperty.Value.ToString()); }
                //    if (itemProperty.Name == "loc_key") { rarity.Value = int.Parse(itemProperty.Value.ToString()); }
                //}
            }

            return rarities;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DotaSchemaRarity).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}