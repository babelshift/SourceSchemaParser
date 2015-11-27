using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    /// <summary>
    /// Represents the root of the tree of tokens in a VDF file.
    /// </summary>
    internal class VRootToken : VToken
    {
        public VKeyValueCollection KeyValuePairs { get; private set; }

        public VRootToken(VKeyValueCollection collection) : base(VTokenType.Root)
        {
            KeyValuePairs = collection;
        }
    }
}
