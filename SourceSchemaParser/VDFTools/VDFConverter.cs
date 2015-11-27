using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SourceSchemaParser.VDFTools
{
    public static class VDFConverter
    {
        public static string ToJson(string path)
        {
            var lines = File.ReadAllLines(path);

            string regexKeyValuePattern = "^\"((?:\\\\.|[^\"\\\\])*)\"[ \\t]*\"((?:\\\\.|[^\"\\\\])*)(\")?";
            Regex regexKeyValue = new Regex(regexKeyValuePattern, RegexOptions.Multiline);

            string regexKeyPattern = "^\"((?:\\\\.|[^\\\\\"])*)\"";
            Regex regexKey = new Regex(regexKeyPattern, RegexOptions.Multiline);

            Stack<VToken> tokens = new Stack<VToken>();
            VRootToken rootCollection = null;

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();

                // skip empty lines and comments;
                if (String.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("/"))
                {
                    continue;
                }

                if (trimmedLine.StartsWith("{"))
                {
                    // need to check for closing bracket matching with opening bracket
                    continue;
                }

                if (trimmedLine.StartsWith("}"))
                {
                    var top = tokens.Pop() as VKeyValueCollection;
                    if (tokens.Count == 0)
                    {
                        // we hit the root, we're done
                        rootCollection = new VRootToken(top);
                        break;
                    }

                    var parentToken = tokens.Peek();
                    if (parentToken.TokenType == VTokenType.KeyValueCollection)
                    {
                        var parentCollection = parentToken as VKeyValueCollection;
                        parentCollection.AddToken(top);
                    }
                }

                // line starts with a quote, key or key/value
                if (trimmedLine.StartsWith("\""))
                {
                    var keyValueMatches = regexKeyValue.Matches(trimmedLine);
                    var keyMatches = regexKey.Matches(trimmedLine);

                    // found a key/value, add it to our parent key/value collection
                    if (keyValueMatches.Count > 0)
                    {
                        string key = keyValueMatches[0].Groups[1].Value;
                        string value = keyValueMatches[0].Groups[2].Value;

                        var parentToken = tokens.Peek();
                        if (parentToken.TokenType == VTokenType.KeyValueCollection)
                        {
                            var parentCollection = parentToken as VKeyValueCollection;
                            parentCollection.AddKeyValuePair(key, value);
                        }
                    }
                    // found a key, start a new key/value collection
                    else if (keyMatches.Count > 0)
                    {
                        string key = keyMatches[0].Groups[1].Value;
                        VKeyValueCollection c = new VKeyValueCollection(key);
                        tokens.Push(c);
                    }
                }
            }

            return JsonConvert.SerializeObject(rootCollection);
        }
    }
}
