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

        public VToken(VTokenType tokenType)
        {
            TokenType = tokenType;
        }
    }
}