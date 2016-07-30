using System.Collections.Generic;

namespace SourceSchemaParser.Dota2
{
    internal class DotaItemBuildGroupSchemaItem
    {
        public string Name { get; set; }
        public IList<string> Items { get; set; }
    }
}