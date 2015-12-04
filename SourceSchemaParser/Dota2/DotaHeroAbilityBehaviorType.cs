﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public sealed class DotaHeroAbilityBehaviorType : DotaEnumType
    {
        public static readonly DotaHeroAbilityBehaviorType HIDDEN = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_HIDDEN", "Hidden", "This ability can be owned by a unit but can't be casted and wont show up on the HUD.");
        public static readonly DotaHeroAbilityBehaviorType PASSIVE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_PASSIVE", "Passive", "Can't be casted but shows up on the ability HUD.");
        public static readonly DotaHeroAbilityBehaviorType NO_TARGET = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_NO_TARGET", "No Target", "Doesn't need a target to be cast, ability fires off as soon as the button is pressed.");
        public static readonly DotaHeroAbilityBehaviorType UNIT_TARGET = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_UNIT_TARGET", "Requires Unit Target", "Ability needs a target to be casted on.");
        public static readonly DotaHeroAbilityBehaviorType POINT = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_POINT", "Requires Cursor Target", "Ability can be cast anywhere the mouse cursor is (If a unit is clicked it will just be cast where the unit was standing).");
        public static readonly DotaHeroAbilityBehaviorType AOE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_AOE", "Area of Effect", "This ability draws a radius where the ability will have effect. YOU STILL NEED A TARGETTING BEHAVIOR LIKE DOTA_ABILITY_BEHAVIOR_POINT FOR THIS TO WORK.");
        public static readonly DotaHeroAbilityBehaviorType NOT_LEARNABLE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_NOT_LEARNABLE", "Not Learnable", "This abillity is channelled. If the user moves or is silenced the ability is interrupted.");
        public static readonly DotaHeroAbilityBehaviorType CHANNELLED = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_CHANNELLED", "Channelled", "This abillity is channelled. If the user moves or is silenced the ability is interrupted.");
        public static readonly DotaHeroAbilityBehaviorType ITEM = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_ITEM", "On Item", "This ability is tied up to an item.");
        public static readonly DotaHeroAbilityBehaviorType TOGGLE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_TOGGLE", "Toggle", "This ability toggles on and off.");
        public static readonly DotaHeroAbilityBehaviorType IMMEDIATE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_IMMEDIATE", "Immediate", "This ability is casted immediately.");
        public static readonly DotaHeroAbilityBehaviorType ROOT_DISABLES = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_ROOT_DISABLES", "Disables Roots", "This ability breaks the hero free of root effects.");
        public static readonly DotaHeroAbilityBehaviorType DONT_RESUME_MOVEMENT = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_DONT_RESUME_MOVEMENT", "Stops Movement", "This ability stops the hero from moving in their direction when casted.");
        public static readonly DotaHeroAbilityBehaviorType IGNORE_BACKSWING = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_IGNORE_BACKSWING", "Stops Movement", "Causes the spell to ignore the backswing animation. It causes the visual animation to instantly stop once the cast point is reached.");
        public static readonly DotaHeroAbilityBehaviorType DONT_RESUME_ATTACK = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_DONT_RESUME_ATTACK", "Stops Attacking", "This ability stops the hero from attacking their target after casting.");
        public static readonly DotaHeroAbilityBehaviorType IGNORE_PSEUDO_QUEUE = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_IGNORE_PSEUDO_QUEUE", "Ignore Pseudo Queue", "Unknown.");
        public static readonly DotaHeroAbilityBehaviorType AUTOCAST = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_AUTOCAST", "Autocast", "This ability can be set to autocast.");
        public static readonly DotaHeroAbilityBehaviorType IGNORE_CHANNEL = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_IGNORE_CHANNEL", "Ignore Channel", "Unknown.");
        public static readonly DotaHeroAbilityBehaviorType DIRECTIONAL = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_DIRECTIONAL", "Ignore Channel", "This ability has a directional component.");
        public static readonly DotaHeroAbilityBehaviorType AURA = new DotaHeroAbilityBehaviorType("DOTA_ABILITY_BEHAVIOR_AURA", "Aura", "This ability is a passive aura.");

        public DotaHeroAbilityBehaviorType(string key, string displayName, string description)
            : base(key, displayName, description)
        { }
    }
}
