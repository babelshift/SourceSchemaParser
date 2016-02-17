using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    internal sealed class DotaItemShareabilityType : DotaEnumType
    {
        public static readonly DotaItemShareabilityType UNKNOWN = new DotaItemShareabilityType("ITEM_UNKNOWN_SHAREABLE", "Unknown", "The shareability of this item is unknown.");
        public static readonly DotaItemShareabilityType PARTIALLY_SHAREABLE = new DotaItemShareabilityType("ITEM_PARTIALLY_SHAREABLE", "Partially", "This item is partially shareable.");
        public static readonly DotaItemShareabilityType FULLY_SHAREABLE = new DotaItemShareabilityType("ITEM_FULLY_SHAREABLE", "Fully", "This item is fully shareable.");
        public static readonly DotaItemShareabilityType FULLY_SHAREABLE_STACKING = new DotaItemShareabilityType("ITEM_FULLY_SHAREABLE_STACKING", "Fully (stacking)", "This item is fully shareable in stacking quantities.");

        public DotaItemShareabilityType(string key, string displayName, string description)
            : base(key, displayName, description)
        { }
    }
}
