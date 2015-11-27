using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    /// <summary>
    /// Represents a collection of key/value pairs in a VDF file.
    /// </summary>
    internal class VKeyValueCollection : VToken
    {
        private List<VToken> keyValuePairs = new List<VToken>();

        /// <summary>
        /// Key of the key/value pair collection.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// List of all key/value pair children that belong to this collection.
        /// </summary>
        public IList<VToken> KeyValuePairs { get { return keyValuePairs; } }

        public VKeyValueCollection(string key) : base(VTokenType.KeyValueCollection)
        {
            Key = key;
        }

        /// <summary>
        /// Adds a new key/value pair to the key/value collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddKeyValuePair(string key, string value)
        {
            keyValuePairs.Add(new VKeyValuePair(key, value));
        }

        /// <summary>
        /// Adds a new token of any type to the collection.
        /// </summary>
        /// <param name="token"></param>
        public void AddToken(VToken token)
        {
            keyValuePairs.Add(token);
        }
    }
}
