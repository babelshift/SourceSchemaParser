using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    internal class VKeyValueCollection : VToken
    {
        private List<VToken> keyValuePairs = new List<VToken>();

        public string Key { get; private set; }

        public IList<VToken> KeyValuePairs { get { return keyValuePairs; } }

        public VKeyValueCollection(string key) : base(VTokenType.KeyValueCollection)
        {
            Key = key;
        }

        public void AddKeyValuePair(string key, string value)
        {
            keyValuePairs.Add(new VKeyValuePair(key, value));
        }

        public void AddToken(VToken token)
        {
            keyValuePairs.Add(token);
        }
    }
}
