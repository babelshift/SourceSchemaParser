using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaItemBuildGroupSchemaItemJsonConverter : JsonConverter
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

            List<DotaItemBuildGroupSchemaItem> itemBuildGroups = new List<DotaItemBuildGroupSchemaItem>();

            var itemBuildGroupProperties = t.Children<JProperty>();
            foreach (var itemBuildGroupProperty in itemBuildGroupProperties)
            {
                DotaItemBuildGroupSchemaItem itemBuildGroup = new DotaItemBuildGroupSchemaItem();
                itemBuildGroup.Name = itemBuildGroupProperty.Name;

                List<string> items = new List<string>();

                var itemProperties = itemBuildGroupProperty.Value.Children<JProperty>();
                foreach (var itemProperty in itemProperties)
                {
                    items.Add(itemProperty.Value.ToString());
                }

                itemBuildGroup.Items = items;

                itemBuildGroups.Add(itemBuildGroup);
            }

            return itemBuildGroups;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DotaItemBuildGroupSchemaItem).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}