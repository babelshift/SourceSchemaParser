using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    /// <summary>
    /// Represents a key/value pair token in a VDF file.
    /// </summary>
    internal class VKeyValuePair : VToken
    {
        /// <summary>
        /// The value of the key/value pair. This is an 'object' because strongly typing would disallow having a single key token having a collection of different types in the same collection.
        /// </summary>
        public object Value { get; private set; }

        public VKeyValuePair(string key, object value) : base(key, VTokenType.KeyValuePair)
        {
            Value = value;
        }
    }
}
