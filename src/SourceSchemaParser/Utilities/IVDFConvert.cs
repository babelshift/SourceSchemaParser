using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SourceSchemaParser.Utilities
{
    public interface IVDFConvert
    {
        T DeserializeObject<T>(IReadOnlyList<string> vdf);
        JObject ToJObject(IReadOnlyList<string> vdf);
        string ToJson(IReadOnlyList<string> vdf);
    }
}