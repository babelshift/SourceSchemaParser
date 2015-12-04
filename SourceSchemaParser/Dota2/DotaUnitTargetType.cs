using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public sealed class DotaUnitTargetType : DotaEnumType
    {
        public static readonly DotaUnitTargetType UNKNOWN = new DotaUnitTargetType("DOTA_UNIT_TARGET_UNKNOWN", "Unknown", "This unit target type is unknown.");
        public static readonly DotaUnitTargetType HERO = new DotaUnitTargetType("DOTA_UNIT_TARGET_HERO", "Hero", "The target of the ability can be a hero.");
        public static readonly DotaUnitTargetType BASIC = new DotaUnitTargetType("DOTA_UNIT_TARGET_BASIC", "Basic", "The target of the ability can be any basic unit.");
        public static readonly DotaUnitTargetType CUSTOM = new DotaUnitTargetType("DOTA_UNIT_TARGET_CUSTOM", "Custom", "The target of the ability is a custom type.");
        public static readonly DotaUnitTargetType BUILDING = new DotaUnitTargetType("DOTA_UNIT_TARGET_BUILDING", "Building", "The target of the ability can be a building.");

        public DotaUnitTargetType(string key, string displayName, string description)
            : base(key, displayName, description)
        { }
    }
}
