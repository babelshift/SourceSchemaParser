using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;

namespace SourceSchemaParser.JsonConverters
{
    internal class DotaAbilitySpecialSchemaItemJsonConverter : JsonConverter
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

            List<DotaAbilitySpecialSchemaItem> abilitySpecials = new List<DotaAbilitySpecialSchemaItem>();

            // load whatever token we are reading and get a reference to all its child properties
            // these will be the useless "01", "02", "03" indices in the valve data format files
            JToken t = JToken.Load(reader);
            var abilitySpecialProperties = t.Children<JProperty>();

            foreach (var abilitySpecialProperty in abilitySpecialProperties)
            {
                // skip over the version property
                if (abilitySpecialProperty.Name == "Version")
                {
                    continue;
                }

                string currentVarType = String.Empty;

                // we need to go through all the actual values of the special properties
                var abilitySpecialIndividualProperties = abilitySpecialProperty.Value.Children<JProperty>();

                foreach (var abilitySpecialIndividualProperty in abilitySpecialIndividualProperties)
                {
                    // record the var_type of this special
                    if (abilitySpecialIndividualProperty.Name == "var_type")
                    {
                        currentVarType = abilitySpecialIndividualProperty.Value.ToString();
                        continue;
                    }

                    // construct the special attribute
                    DotaAbilitySpecialSchemaItem abilitySpecial = new DotaAbilitySpecialSchemaItem()
                    {
                        Name = abilitySpecialIndividualProperty.Name,
                        Value = abilitySpecialIndividualProperty.Value.ToString(),
                        VarType = currentVarType
                    };

                    abilitySpecials.Add(abilitySpecial);
                }
            }

            return abilitySpecials;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaAbilitySpecialSchemaItem>).IsAssignableFrom(objectType);
        }
    }
}