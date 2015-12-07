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
        public static string ToJson(string[] vdfTextLines)
        {
            Regex regexKey = new Regex("^\"((?:\\\\.|[^\\\\\"])*)\"", RegexOptions.Multiline);
            Regex regexKeyValue = new Regex("^\"((?:\\\\.|[^\"\\\\])*)\"[ \\t]*\"((?:\\\\.|[^\"\\\\])*)(\")?", RegexOptions.Multiline);

            Stack<VToken> tokens = new Stack<VToken>();
            VRootToken rootCollection = null;
            bool expectOpenBrace = false;

            for (int i = 0; i < vdfTextLines.Length; i++)
            {
                // get rid of white spaces on the ends of each line
                string trimmedLine = vdfTextLines[i].Trim();

                // skip empty lines and comments;
                if (String.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("/"))
                {
                    continue;
                }

                // if we see an open brace, reset that we are expecting to see it and continue to the next token
                if (trimmedLine.StartsWith("{"))
                {
                    expectOpenBrace = false;
                    continue;
                }

                // if we are expecting an open brace at this point, the tree is unbalanced
                if (expectOpenBrace)
                {
                    throw new InvalidOperationException("Could not parse the VDF because a an opening '{' is missing.");
                }

                // if we see a closing brace, the key/value collection has ended, so we pop off our collection and add it to our parent key
                if (trimmedLine.StartsWith("}"))
                {
                    // get the key/value collection on top of the stack
                    var top = tokens.Pop() as VKeyValueCollection;

                    // if there's nothing left in the stack, we hit the root of the VDF, so we are done parsing
                    if (tokens.Count == 0)
                    {
                        rootCollection = new VRootToken(top);
                        break;
                    }

                    // the parent is now on top of the stack after popping its child
                    var parentToken = tokens.Peek();
                    if (parentToken.TokenType == VTokenType.KeyValueCollection)
                    {
                        // add the previous top of the stack to the parent's collection of key/values
                        var parentCollection = parentToken as VKeyValueCollection;
                        parentCollection.AddToken(top);
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not parse the VDF because tokens can only be children of a token collection.");
                    }
                }

                // if we see a quote at the start, we are parsing either a key or a key/value pair
                if (trimmedLine.StartsWith("\""))
                {
                    while (true)
                    {
                        var keyValueMatches = regexKeyValue.Matches(trimmedLine);
                        var keyMatches = regexKey.Matches(trimmedLine);

                        // we see a key/value pair, so add it to the token collection on top of the stack
                        if (keyValueMatches.Count > 0)
                        {
                            // this line did not include an ending quote, so we need to loop forever and build up the multi-line value until we find one
                            if (keyValueMatches[0].Groups.Count < 4 || keyValueMatches[0].Groups[3] == null || String.IsNullOrEmpty(keyValueMatches[0].Groups[3].Value))
                            {
                                trimmedLine += Environment.NewLine + vdfTextLines[++i].Trim();
                                continue;
                            }

                            string key = keyValueMatches[0].Groups[1].Value;
                            string value = keyValueMatches[0].Groups[2].Value;

                            var parentToken = tokens.Peek();
                            if (parentToken.TokenType == VTokenType.KeyValueCollection)
                            {
                                var parentCollection = parentToken as VKeyValueCollection;
                                parentCollection.AddKeyValuePair(key, value);
                            }
                            else
                            {
                                throw new InvalidOperationException("Could not parse the VDF because tokens can only be children of a token collection.");
                            }
                        }
                        // we see a key, so create a new key/value collection and add it to our stack
                        else if (keyMatches.Count > 0)
                        {
                            string key = keyMatches[0].Groups[1].Value;
                            VKeyValueCollection c = new VKeyValueCollection(key);
                            tokens.Push(c);
                            expectOpenBrace = true;
                        }
                        else
                        {
                            throw new InvalidOperationException("Could not parse VDF because the format is incorrect.");
                        }

                        break;
                    }
                }
            }

            // if there's anything left on the stack, we are dealing with an unbalanced tree
            if (tokens.Count > 0)
            {
                throw new InvalidOperationException("Could not parse VDF because it's unbalanced. Check for matching opening and closing braces.");
            }

            return JsonConvert.SerializeObject(rootCollection);
        }

        /// <summary>
        /// Given a string containing VDF formatted text, it will be parsed and converted JSON. Currently only supports VDF files in which all tokens are on
        /// separate lines. That is, '{', '}', '"key"', and '"key" "value"' tokens must be on separate lines until I come up with a better parser for different 
        /// formatting styles.
        /// </summary>
        /// <param name="path">Path to the file that we want to parse.</param>
        /// <returns></returns>
        public static string ToJson(string vdfText)
        {
            var lines = vdfText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            return ToJson(lines);
        }
    }
}
