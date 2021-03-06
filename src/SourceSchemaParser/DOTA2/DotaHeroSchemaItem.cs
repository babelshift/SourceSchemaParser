﻿using Newtonsoft.Json;
using SourceSchemaParser.JsonConverters;

namespace SourceSchemaParser.DOTA2
{
    internal class DotaHeroSchemaItem
    {
        [JsonProperty("HeroID")]
        public uint HeroId { get; set; }

        public string Name { get; set; }

        [JsonProperty("BaseClass")]
        public string BaseClass { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("SoundSet")]
        public string SoundSet { get; set; }

        [JsonProperty("Enabled")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool Enabled { get; set; }

        [JsonProperty("Level")]
        public string Level { get; set; }

        [JsonProperty("BotImplemented")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool BotImplemented { get; set; }

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

        [JsonProperty("Ability9")]
        public string Ability9 { get; set; }

        [JsonProperty("Ability10")]
        public string Ability10 { get; set; }

        [JsonProperty("Ability11")]
        public string Ability11 { get; set; }

        [JsonProperty("Ability12")]
        public string Ability12 { get; set; }

        [JsonProperty("Ability13")]
        public string Ability13 { get; set; }

        [JsonProperty("Ability14")]
        public string Ability14 { get; set; }

        [JsonProperty("Ability15")]
        public string Ability15 { get; set; }

        [JsonProperty("Ability16")]
        public string Ability16 { get; set; }

        [JsonProperty("Ability17")]
        public string Ability17 { get; set; }

        [JsonProperty("Ability18")]
        public string Ability18 { get; set; }
        
        [JsonProperty("Ability19")]
        public string Ability19 { get; set; }
        
        [JsonProperty("Ability20")]
        public string Ability20 { get; set; }

        [JsonProperty("Ability21")]
        public string Ability21 { get; set; }

        [JsonProperty("Ability22")]
        public string Ability22 { get; set; }

        [JsonProperty("Ability23")]
        public string Ability23 { get; set; }

        [JsonProperty("Ability24")]
        public string Ability24 { get; set; }

        [JsonProperty("ArmorPhysical")]
        public double ArmorPhysical { get; set; }

        [JsonProperty("MagicalResistance")]
        public string MagicalResistance { get; set; }

        [JsonProperty("AttackCapabilities")]
        public string AttackCapabilities { get; set; }

        [JsonProperty("AttackDamageMin")]
        public uint AttackDamageMin { get; set; }

        [JsonProperty("AttackDamageMax")]
        public uint AttackDamageMax { get; set; }

        [JsonProperty("AttackDamageType")]
        public string AttackDamageType { get; set; }

        [JsonProperty("AttackRate")]
        public double AttackRate { get; set; }

        [JsonProperty("AttackAnimationPoint")]
        public double AttackAnimationPoint { get; set; }

        [JsonProperty("AttackAcquisitionRange")]
        public uint AttackAcquisitionRange { get; set; }

        [JsonProperty("AttackRange")]
        public uint AttackRange { get; set; }

        [JsonProperty("ProjectileModel")]
        public string ProjectileModel { get; set; }

        [JsonProperty("ProjectileSpeed")]
        public uint ProjectileSpeed { get; set; }

        [JsonProperty("AttributePrimary")]
        public string AttributePrimary { get; set; }

        [JsonProperty("AttributeBaseStrength")]
        public uint AttributeBaseStrength { get; set; }

        [JsonProperty("AttributeStrengthGain")]
        public double AttributeStrengthGain { get; set; }

        [JsonProperty("AttributeBaseIntelligence")]
        public uint AttributeBaseIntelligence { get; set; }

        [JsonProperty("AttributeIntelligenceGain")]
        public double AttributeIntelligenceGain { get; set; }

        [JsonProperty("AttributeBaseAgility")]
        public uint AttributeBaseAgility { get; set; }

        [JsonProperty("AttributeAgilityGain")]
        public double AttributeAgilityGain { get; set; }

        [JsonProperty("BountyXP")]
        public uint BountyXP { get; set; }

        [JsonProperty("BountyGoldMin")]
        public uint BountyGoldMin { get; set; }

        [JsonProperty("BountyGoldMax")]
        public uint BountyGoldMax { get; set; }

        [JsonProperty("BoundsHullName")]
        public string BoundsHullName { get; set; }

        [JsonProperty("RingRadius")]
        public uint RingRadius { get; set; }

        [JsonProperty("MovementCapabilities")]
        public string MovementCapabilities { get; set; }

        [JsonProperty("MovementSpeed")]
        public uint MovementSpeed { get; set; }

        [JsonProperty("MovementTurnRate")]
        public double MovementTurnRate { get; set; }

        [JsonProperty("HasAggressiveStance")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool HasAggressiveStance { get; set; }

        [JsonProperty("StatusHealth")]
        public uint StatusHealth { get; set; }

        [JsonProperty("StatusHealthRegen")]
        public double StatusHealthRegen { get; set; }

        [JsonProperty("StatusMana")]
        public uint StatusMana { get; set; }

        [JsonProperty("StatusManaRegen")]
        public double StatusManaRegen { get; set; }

        [JsonProperty("TeamName")]
        public string TeamName { get; set; }

        [JsonProperty("Team")]
        public string Team { get; set; }

        [JsonProperty("CombatClassAttack")]
        public string CombatClassAttack { get; set; }

        [JsonProperty("CombatClassDefend")]
        public string CombatClassDefend { get; set; }

        [JsonProperty("UnitRelationshipClass")]
        public string UnitRelationshipClass { get; set; }

        [JsonProperty("VisionDaytimeRange")]
        public uint VisionDaytimeRange { get; set; }

        [JsonProperty("VisionNighttimeRange")]
        public uint VisionNighttimeRange { get; set; }

        [JsonProperty("HasInventory")]
        [JsonConverter(typeof(StringToBoolJsonConverter))]
        public bool HasInventory { get; set; }

        [JsonProperty("VoiceBackgroundSound")]
        public string VoiceBackgroundSound { get; set; }

        [JsonProperty("HealthBarOffset")]
        public int HealthBarOffset { get; set; }

        [JsonProperty("IdleExpression")]
        public string IdleExpression { get; set; }

        [JsonProperty("IdleSoundLoop")]
        public string IdleSoundLoop { get; set; }

        [JsonProperty("AbilityDraftDisabled")]
        public string AbilityDraftDisabled { get; set; }

        [JsonProperty("ARDMDisabled")]
        public string ARDMDisabled { get; set; }

        [JsonProperty("workshop_guide_name")]
        public string Url { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("rolelevels")]
        public string RoleLevels { get; set; }

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