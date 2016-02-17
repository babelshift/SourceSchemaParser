﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    internal sealed class DotaHeroPrimaryAttributeType : DotaEnumType
    {
        public static readonly DotaHeroPrimaryAttributeType STRENGTH = new DotaHeroPrimaryAttributeType("DOTA_ATTRIBUTE_STRENGTH", "Strength", "Strength health and health regeneration.");
        public static readonly DotaHeroPrimaryAttributeType AGILITY = new DotaHeroPrimaryAttributeType("DOTA_ATTRIBUTE_AGILITY", "Agility", "Agility increases armor.");
        public static readonly DotaHeroPrimaryAttributeType INTELLECT = new DotaHeroPrimaryAttributeType("DOTA_ATTRIBUTE_INTELLECT", "Intellect", "Intellect increases mana and mana regeneration.");

        public DotaHeroPrimaryAttributeType(string key, string displayName, string description)
            : base(key, displayName, description)
        { }
    }
}
