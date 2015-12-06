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
    public class DotaItemAbilitySchemaItem
    {
        public DotaItemAbilitySchemaItem()
        {
            AbilitySpecials = new List<DotaAbilitySpecialSchemaItem>();
        }

        [JsonProperty("ID")]
        public int Id { get; set; }

        public string Name { get; set; }
        
        [JsonProperty("AbilityBehavior")]
        public string AbilityBehavior { get; set; }
        
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

        [JsonProperty("AbilityUnitTargetTeam")]
        public string AbilityUnitTargetTeam { get; set; }

        [JsonProperty("AbilityUnitDamageType")]
        public string AbilityUnitDamageType { get; set; }

        [JsonProperty("AbilityUnitTargetFlags")]
        public string AbilityUnitTargetFlags { get; set; }
        
        [JsonProperty("AbilityUnitTargetType")]
        public string AbilityUnitTargetType { get; set; }

        [JsonProperty("AbilitySpecial")]
        [JsonConverter(typeof(DotaAbilitySpecialSchemaItemJsonConverter))]
        public IList<DotaAbilitySpecialSchemaItem> AbilitySpecials { get; set; }

        [JsonProperty("ItemCost")]
        public int ItemCost { get; set; }

        [JsonProperty("ItemShopTags")]
        public string ItemShopTags { get; set; }

        [JsonProperty("ItemQuality")]
        public string ItemQuality { get; set; }

        [JsonProperty("ItemStackable")]
        public string ItemStackable { get; set; }

        [JsonProperty("ItemShareability")]
        public string ItemShareability { get; set; }

        [JsonProperty("ItemPermanent")]
        public string ItemPermanent { get; set; }

        [JsonProperty("ItemInitialCharges")]
        public int ItemInitialCharges { get; set; }

        [JsonProperty("ItemPurchasable")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemPurchasable { get; set; }

        [JsonProperty("ItemSellable")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemSellable { get; set; }

        [JsonProperty("ItemKillable")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemKillable { get; set; }

        [JsonProperty("ItemDeclarations")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemDeclarations { get; set; }

        [JsonProperty("ItemCastOnPickup")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemCastOnPickup { get; set; }
    }
}
