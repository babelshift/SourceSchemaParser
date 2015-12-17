﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.Dota2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaSchemaUsedByHeroesJsonConverter : JsonConverter
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

            List<string> heroes = new List<string>();

            var usedByHeroesProperties = t.Children<JProperty>();
            foreach (var usedByHeroesProperty in usedByHeroesProperties)
            {
                heroes.Add(usedByHeroesProperty.Name);
            }

            return heroes;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<string>).IsAssignableFrom(objectType);
        }
    }
}