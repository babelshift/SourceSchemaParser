using Newtonsoft.Json;

namespace SourceSchemaParser.VDFTools
{
    internal enum VTokenType
    {
        KeyValuePair,
        KeyValueCollection,
        Root
    }

    /// <summary>
    /// Represents a generic token found in a VDF file such as a key or key/value pair
    /// </summary>
    [JsonConverter(typeof(VRootTokenJsonConverter))]
    internal abstract class VToken
    {
        public VTokenType TokenType { get; private set; }

        /// <summary>
        /// The key of the key/value pair
        /// </summary>
        public string Key { get; private set; }

        public VToken(string key)
        {
            Key = key;
        }

        public VToken(string key, VTokenType tokenType) : this(key)
        {
            TokenType = tokenType;
        }
    }
}