using System;
using System.Collections.Generic;

namespace SourceSchemaParser.Utilities
{
    /// <summary>
    /// Represents a collection of key/value pairs in a VDF file.
    /// </summary>
    internal class VKeyValueCollection : VToken
    {
        private Dictionary<string, int> duplicateKeyCounts = new Dictionary<string, int>();

        private Dictionary<string, VToken> tokens = new Dictionary<string, VToken>();

        /// <summary>
        /// List of all key/value pair children that belong to this collection.
        /// </summary>
        public IDictionary<string, VToken> KeyValuePairs { get { return tokens; } }

        public VKeyValueCollection(string key) : base(key, VTokenType.KeyValueCollection)
        {
        }

        /// <summary>
        /// Adds a new key/value pair to the key/value collection.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddKeyValuePair(string key, string value)
        {
            string uniqueKey = GetUniqueKey(key);

            tokens.Add(uniqueKey, new VKeyValuePair(uniqueKey, value));
        }

        private string GetUniqueKey(string key)
        {
            string uniqueKey = key;

            VToken token = null;
            bool keyExists = tokens.TryGetValue(key, out token);
            if (keyExists)
            {
                // try to get the count of this duplicate key so we can roll the number and add appropriately
                int count = 0;
                bool success = duplicateKeyCounts.TryGetValue(key, out count);
                count++;

                if (success)
                {
                    // increase the duplicate count
                    duplicateKeyCounts[key] = count;
                }
                else
                {
                    duplicateKeyCounts.Add(key, count);
                }

                // create our new unique key with the appended increased count
                uniqueKey = String.Format("{0}-{1}", key, count);
            }

            return uniqueKey;
        }

        /// <summary>
        /// Adds a new token of any type to the collection.
        /// </summary>
        /// <param name="token"></param>
        public void AddToken(VToken token)
        {
            string uniqueKey = GetUniqueKey(token.Key);

            tokens.Add(uniqueKey, token);
        }
    }
}