using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SourceSchemaParser.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public class DotaAbilitySchemaItem
    {
        [JsonProperty("ID")]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("AbilityType")]
        public string AbilityType { get; set; }

        [JsonProperty("AbilityBehavior")]
        public string AbilityBehavior { get; set; }

        [JsonProperty("OnCastbar")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool OnCastbar { get; set; }

        [JsonProperty("OnLearnbar")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool OnLearnbar { get; set; }

        [JsonProperty("FightRecapLevel")]
        public string FightRecapLevel { get; set; }

        [JsonProperty("AbilityCastRange")]
        public string AbilityCastRange { get; set; }

        [JsonProperty("AbilityRangeBuffer")]
        public int AbilityRangeBuffer { get; set; }

        [JsonProperty("AbilityCastPoint")]
        public string AbilityCastPoint{ get; set; }

        [JsonProperty("AbilityChannelTime")]
        public string AbilityChannelTime { get; set; }

        [JsonProperty("AbilityCooldown")]
        public string AbilityCooldown { get; set; }

        [JsonProperty("AbilityDuration")]
        public string AbilityDuration { get; set; }

        [JsonProperty("AbilitySharedCooldown")]
        public string AbilitySharedCooldown { get; set; }

        [JsonProperty("AbilityDamage")]
        public string AbilityDamage { get; set; }

        [JsonProperty("AbilityManaCost")]
        public string AbilityManaCost { get; set; }

        [JsonProperty("AbilityModifierSupportValue")]
        public double AbilityModifierSupportValue { get; set; }

        [JsonProperty("AbilityModifierSupportBonus")]
        public double AbilityModifierSupportBonus { get; set; }

        [JsonProperty("AbilityUnitTargetTeam")]
        public string AbilityUnitTargetTeam { get; set; }

        [JsonProperty("AbilityUnitDamageType")]
        public string AbilityUnitDamageType { get; set; }

        [JsonProperty("SpellImmunityType")]
        public string SpellImmunityType { get; set; }

        [JsonProperty("AbilityUnitTargetFlags")]
        public string AbilityUnitTargetFlags { get; set; }
        
        [JsonProperty("AbilityUnitTargetType")]
        public string AbilityUnitTargetType { get; set; }

        [JsonProperty("AbilitySpecial")]
        [JsonConverter(typeof(DotaAbilitySpecialSchemaItemJsonConverter))]
        public IList<DotaAbilitySpecialSchemaItem> AbilitySpecials { get; set; }
    }
}
