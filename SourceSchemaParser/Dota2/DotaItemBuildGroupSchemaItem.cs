using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public class DotaItemBuildGroupSchemaItem
    {
        public string Name { get; set; }
        public IList<string> Items { get; set; }
    }
}
