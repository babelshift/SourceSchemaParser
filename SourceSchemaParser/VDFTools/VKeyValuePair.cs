using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    internal class VKeyValuePair : VToken
    {
        public string Key { get; private set; }
        public object Value { get; private set; }

        public VKeyValuePair(string key, object value) : base(VTokenType.KeyValuePair)
        {
            Key = key;
            Value = value;
        }
    }
}
