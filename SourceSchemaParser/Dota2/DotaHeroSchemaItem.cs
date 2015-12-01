using Newtonsoft.Json;

namespace SourceSchemaParser.Dota2
{
    public class DotaHeroSchemaItem
    {
        [JsonProperty("BaseClass")]
        public string BaseClass { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("SoundSet")]
        public string SoundSet { get; set; }

        [JsonProperty("Enabled")]
        public string Enabled { get; set; }

        [JsonProperty("Level")]
        public string Level { get; set; }

        [JsonProperty("BotImplemented")]
        public string BotImplemented { get; set; }

        [JsonProperty("NewHero")]
        public string NewHero { get; set; }

        [JsonProperty("HeroPool1")]
        public string HeroPool1 { get; set; }

        [JsonProperty("HeroUnlockOrder")]
        public string HeroUnlockOrder { get; set; }

        [JsonProperty("CMEnabled")]
        public string CMEnabled { get; set; }

        [JsonProperty("CMTournamentIgnore")]
        public string CMTournamentIgnore { get; set; }

        [JsonProperty("new_player_enable")]
        public string NewPlayerEnable { get; set; }

        [JsonProperty("AbilityLayout")]
        public string AbilityLayout { get; set; }

        [JsonProperty("Ability1")]
        public string Ability1 { get; set; }

        [JsonProperty("Ability2")]
        public string Ability2 { get; set; }

        [JsonProperty("Ability3")]
        public string Ability3 { get; set; }

        [JsonProperty("Ability4")]
        public string Ability4 { get; set; }

        [JsonProperty("Ability5")]
        public string Ability5 { get; set; }

        [JsonProperty("Ability6")]
        public string Ability6 { get; set; }

        [JsonProperty("Ability7")]
        public string Ability7 { get; set; }

        [JsonProperty("Ability8")]
        public string Ability8 { get; set; }

        [JsonProperty("ArmorPhysical")]
        public string ArmorPhysical { get; set; }

        [JsonProperty("MagicalResistance")]
        public string MagicalResistance { get; set; }

        [JsonProperty("AttackCapabilities")]
        public string AttackCapabilities { get; set; }

        [JsonProperty("AttackDamageMin")]
        public string AttackDamageMin { get; set; }

        [JsonProperty("AttackDamageMax")]
        public string AttackDamageMax { get; set; }

        [JsonProperty("AttackDamageType")]
        public string AttackDamageType { get; set; }

        [JsonProperty("AttackRate")]
        public string AttackRate { get; set; }

        [JsonProperty("AttackAnimationPoint")]
        public string AttackAnimationPoint { get; set; }

        [JsonProperty("AttackAcquisitionRange")]
        public string AttackAcquisitionRange { get; set; }

        [JsonProperty("AttackRange")]
        public string AttackRange { get; set; }

        [JsonProperty("ProjectileModel")]
        public string ProjectileModel { get; set; }

        [JsonProperty("ProjectileSpeed")]
        public string ProjectileSpeed { get; set; }

        [JsonProperty("AttributePrimary")]
        public string AttributePrimary { get; set; }

        [JsonProperty("AttributeBaseStrength")]
        public string AttributeBaseStrength { get; set; }

        [JsonProperty("AttributeStrengthGain")]
        public string AttributeStrengthGain { get; set; }

        [JsonProperty("AttributeBaseIntelligence")]
        public string AttributeBaseIntelligence { get; set; }

        [JsonProperty("AttributeIntelligenceGain")]
        public string AttributeIntelligenceGain { get; set; }

        [JsonProperty("AttributeBaseAgility")]
        public string AttributeBaseAgility { get; set; }

        [JsonProperty("AttributeAgilityGain")]
        public string AttributeAgilityGain { get; set; }

        [JsonProperty("BountyXP")]
        public string BountyXP { get; set; }

        [JsonProperty("BountyGoldMin")]
        public string BountyGoldMin { get; set; }

        [JsonProperty("BountyGoldMax")]
        public string BountyGoldMax { get; set; }

        [JsonProperty("BoundsHullName")]
        public string BoundsHullName { get; set; }

        [JsonProperty("RingRadius")]
        public string RingRadius { get; set; }

        [JsonProperty("MovementCapabilities")]
        public string MovementCapabilities { get; set; }

        [JsonProperty("MovementSpeed")]
        public string MovementSpeed { get; set; }

        [JsonProperty("MovementTurnRate")]
        public string MovementTurnRate { get; set; }

        [JsonProperty("HasAggressiveStance")]
        public string HasAggressiveStance { get; set; }

        [JsonProperty("StatusHealth")]
        public string StatusHealth { get; set; }

        [JsonProperty("StatusHealthRegen")]
        public string StatusHealthRegen { get; set; }

        [JsonProperty("StatusMana")]
        public string StatusMana { get; set; }

        [JsonProperty("StatusManaRegen")]
        public string StatusManaRegen { get; set; }

        [JsonProperty("TeamName")]
        public string TeamName { get; set; }

        [JsonProperty("CombatClassAttack")]
        public string CombatClassAttack { get; set; }

        [JsonProperty("CombatClassDefend")]
        public string CombatClassDefend { get; set; }

        [JsonProperty("UnitRelationshipClass")]
        public string UnitRelationshipClass { get; set; }

        [JsonProperty("VisionDaytimeRange")]
        public string VisionDaytimeRange { get; set; }

        [JsonProperty("VisionNighttimeRange")]
        public string VisionNighttimeRange { get; set; }

        [JsonProperty("HasInventory")]
        public string HasInventory { get; set; }

        [JsonProperty("VoiceBackgroundSound")]
        public string VoiceBackgroundSound { get; set; }

        [JsonProperty("HealthBarOffset")]
        public string HealthBarOffset { get; set; }

        [JsonProperty("IdleExpression")]
        public string IdleExpression { get; set; }

        [JsonProperty("IdleSoundLoop")]
        public string IdleSoundLoop { get; set; }

        [JsonProperty("AbilityDraftDisabled")]
        public string AbilityDraftDisabled { get; set; }

        [JsonProperty("ARDMDisabled")]
        public string ARDMDisabled { get; set; }

        //[JsonProperty("HUD")]
        //public HUD HUD { get; set; }
    }

    internal class ItemSlot
    {
        [JsonProperty("SlotIndex")]
        public string SlotIndex { get; set; }

        [JsonProperty("SlotName")]
        public string SlotName { get; set; }

        [JsonProperty("SlotText")]
        public string SlotText { get; set; }

        [JsonProperty("TextureWidth")]
        public string TextureWidth { get; set; }

        [JsonProperty("TextureHeight")]
        public string TextureHeight { get; set; }

        [JsonProperty("MaxPolygonsLOD0")]
        public string MaxPolygonsLOD0 { get; set; }

        [JsonProperty("MaxPolygonsLOD1")]
        public string MaxPolygonsLOD1 { get; set; }
    }

    internal class ItemSlots
    {
        public ItemSlot Slot0 { get; set; }
        public ItemSlot Slot1 { get; set; }
        public ItemSlot Slot2 { get; set; }
        public ItemSlot Slot3 { get; set; }
        public ItemSlot Slot4 { get; set; }
        public ItemSlot Slot5 { get; set; }
        public ItemSlot Slot6 { get; set; }
        public ItemSlot Slot7 { get; set; }
    }
}