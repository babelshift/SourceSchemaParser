using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public sealed class DotaHeroAbilityType : DotaEnumType
    {
        public static readonly DotaHeroAbilityType UNKNOWN = new DotaHeroAbilityType("DOTA_ABILITY_TYPE_UNKNOWN", "Unknown", "This is a an unknown ability type.");
        public static readonly DotaHeroAbilityType BASIC = new DotaHeroAbilityType("DOTA_ABILITY_TYPE_BASIC", "Basic", "This is a basic ability learnable at any level.");
        public static readonly DotaHeroAbilityType ULTIMATE = new DotaHeroAbilityType("DOTA_ABILITY_TYPE_ULTIMATE", "Ultimate", "This is a unique ultimate ability learnable at certain levels.");
        public static readonly DotaHeroAbilityType ATTRIBUTES = new DotaHeroAbilityType("DOTA_ABILITY_TYPE_ATTRIBUTES", "Attributes", "This is an attribute ability to gain stats.");

        public DotaHeroAbilityType(string key, string displayName, string description)
            : base(key, displayName, description)
        { }
    }
}
