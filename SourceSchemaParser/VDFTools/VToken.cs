using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    internal enum VTokenType
    {
        KeyValuePair,
        KeyValueCollection,
        Root
    }

    [JsonConverter(typeof(VRootTokenJsonConverter))]
    internal abstract class VToken
    {
        public VTokenType TokenType { get; private set; }
        public VToken(VTokenType tokenType)
        {
            TokenType = tokenType;
        }
    }
}
