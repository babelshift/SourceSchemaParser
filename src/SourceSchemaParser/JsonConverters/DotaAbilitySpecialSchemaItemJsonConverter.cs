using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceSchemaParser.DOTA2;
using System;
using System.Collections.Generic;
using System.Reflection;

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

                string abilityName = string.Empty;
                string abilityValue = string.Empty;
                string abilityVarType = string.Empty;
                string abilityLinkedSpecialBonus = string.Empty;
                bool abilityRequiresScepter = false;

                // we need to go through all the actual values of the special properties
                var abilitySpecialIndividualProperties = abilitySpecialProperty.Value.Children<JProperty>();

                foreach (var abilitySpecialIndividualProperty in abilitySpecialIndividualProperties)
                {
                    // record the var_type of this special
                    if (abilitySpecialIndividualProperty.Name == "var_type")
                    {
                        abilityVarType = abilitySpecialIndividualProperty.Value.ToString();
                    }
                    else if (abilitySpecialIndividualProperty.Name == "LinkedSpecialBonus")
                    {
                        abilityLinkedSpecialBonus = abilitySpecialIndividualProperty.Value.ToString();
                    }
                    else if (abilitySpecialIndividualProperty.Name == "RequiresScepter")
                    {
                        abilityRequiresScepter = abilitySpecialIndividualProperty.Value.ToString() == "1";
                    }
                    else if (abilitySpecialProperty.Name == "LinkedSpecialBonusOperation")
                    {
                        // no op until we want to handle this
                    }
                    else
                    {
                        abilityName = abilitySpecialIndividualProperty.Name;
                        abilityValue = abilitySpecialIndividualProperty.Value.ToString();
                    }
                }

                // construct the special attribute
                DotaAbilitySpecialSchemaItem abilitySpecial = new DotaAbilitySpecialSchemaItem()
                {
                    Name = abilityName,
                    Value = abilityValue,
                    VarType = abilityVarType,
                    LinkedSpecialBonus = abilityLinkedSpecialBonus,
                    RequiresScepter = abilityRequiresScepter
                };

                abilitySpecials.Add(abilitySpecial);
            }

            return abilitySpecials;
        }

        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<DotaAbilitySpecialSchemaItem>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}