using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SourceSchemaParser.JsonConverters;
using System.Collections.Generic;

namespace SourceSchemaParser.Dota2
{
    internal class DotaItemAbilitySchemaItem
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

        [JsonProperty("AbilityCastPoint")]
        public string AbilityCastPoint { get; set; }

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
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemStackable { get; set; }

        [JsonProperty("ItemShareability")]
        public string ItemShareability { get; set; }

        [JsonProperty("ItemPermanent")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool ItemPermanent { get; set; }

        [JsonProperty("ItemInitialCharges")]
        public int? ItemInitialCharges { get; set; }

        [JsonProperty("ItemDisplayCharges")]
        public int? ItemDisplayCharges { get; set; }

        [JsonProperty("ItemStockMax")]
        public int? ItemStockMax { get; set; }

        [JsonProperty("ItemStockInitial")]
        public int? ItemStockInitial { get; set; }

        [JsonProperty("ItemStockTime")]
        public double? ItemStockTime { get; set; }

        [JsonProperty("ItemPurchasable")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemPurchasable { get; set; }

        [JsonProperty("ItemSellable")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemSellable { get; set; }

        [JsonProperty("ItemKillable")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemKillable { get; set; }

        [JsonProperty("ItemDeclarations")]
        public string ItemDeclarations { get; set; }

        [JsonProperty("ItemCastOnPickup")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemCastOnPickup { get; set; }

        [JsonProperty("ItemSupport")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemSupport { get; set; }

        [JsonProperty("ItemResult")]
        public string ItemResult { get; set; }

        [JsonProperty("ItemAlertable")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemAlertable { get; set; }

        [JsonProperty("ItemDroppable")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemDroppable { get; set; }

        [JsonProperty("ItemContributesToNetWorthWhenDropped")]
        [JsonConverter(typeof(StringToNullableBoolJsonConverter))]
        public bool? ItemContributesToNetWorthWhenDropped { get; set; }

        [JsonProperty("ItemDisassembleRule")]
        public string ItemDisassembleRule { get; set; }
    }
}