using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SourceSchemaParser.JsonConverters;
using SourceSchemaParser.VDFTools;

namespace SourceSchemaParser
{
    public static class SchemaFactory
    {
        #region Public Entry Points

        public static IReadOnlyCollection<DotaLeague> GetDotaLeaguesFromText(string vdfText, string languageFilePath)
        {
            var leaguesFromSchema = GetLeaguesFromItemSchema(vdfText);

            return GetDotaLeagues(languageFilePath, leaguesFromSchema);
        }

        public static IReadOnlyCollection<DotaLeague> GetDotaLeaguesFromFile(string schemaFilePath, string languageFilePath)
        {
            var vdfTextLines = File.ReadAllLines(schemaFilePath);
            var leagesFromSchema = GetLeaguesFromItemSchema(vdfTextLines);

            return GetDotaLeagues(languageFilePath, leagesFromSchema);
        }

        #endregion

        #region Merge Parsed Leagues and Language Tokens

        private static IReadOnlyCollection<DotaLeague> GetDotaLeagues(string languageFilePath, IReadOnlyCollection<DotaSchemaItem> parsedDotaLeagues)
        {
            var tokens = GetLanguageTokensFromLanguageSchema(languageFilePath);

            ReplaceTokensWithLocalizedValues(parsedDotaLeagues, tokens);

            List<DotaLeague> dotaLeagues = FlattenDotaSchemaItemLeagues(parsedDotaLeagues);

            return dotaLeagues;
        }
        
        private static void ReplaceTokensWithLocalizedValues(IReadOnlyCollection<DotaSchemaItem> leagues, IDictionary<string, string> tokens)
        {
            foreach (var league in leagues)
            {
                league.NameLocalized = GetLanguageToken(league.NameLocalized.Remove(0, 1), tokens);
                league.DescriptionLocalized = GetLanguageToken(league.DescriptionLocalized.Remove(0, 1), tokens);
                if (!String.IsNullOrEmpty(league.TypeName))
                {
                    league.TypeName = GetLanguageToken(league.TypeName.Remove(0, 1), tokens);
                }
                else
                {
                    league.TypeName = "Unknown";
                }
            }
        }

        private static string GetLanguageToken(string key, IDictionary<string, string> tokens)
        {
            string localizedValue = String.Empty;
            bool exists = tokens.TryGetValue(key, out localizedValue);
            if (exists)
            {
                return localizedValue;
            }
            else
            {
                return "Unknown";
            }
        }

        private static IDictionary<string, string> GetLanguageTokensFromLanguageSchema(string languageFilePath)
        {
            JObject languageSchema = JObject.Parse(File.ReadAllText(languageFilePath));

            JToken item = languageSchema["lang"]["Tokens"];

            var tokens = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.ToString(), new SchemaLanguageTokensJsonConverter());

            return tokens;
        }


        #endregion

        #region Parse Out Leagues from VDF and JSON

        private static IReadOnlyCollection<DotaSchemaItem> GetLeaguesFromItemSchema(string vdfText)
        {
            string json = VDFConverter.ToJson(vdfText);

            return GetLeaguesFromJson(json);
        }

        private static IReadOnlyCollection<DotaSchemaItem> GetLeaguesFromItemSchema(string[] vdfTextLines)
        {
            string json = VDFConverter.ToJson(vdfTextLines);

            return GetLeaguesFromJson(json);
        }

        private static IReadOnlyCollection<DotaSchemaItem> GetLeaguesFromJson(string json)
        {
            JObject itemSchema = JObject.Parse(json);

            JToken item = itemSchema["items_game"]["items"];

            var leagues = JsonConvert.DeserializeObject<IList<DotaSchemaItem>>(item.ToString(), new SchemaItemsToDotaLeaguesJsonConverter());

            return new ReadOnlyCollection<DotaSchemaItem>(leagues);
        }

        #endregion
        
        private static List<DotaLeague> FlattenDotaSchemaItemLeagues(IReadOnlyCollection<DotaSchemaItem> leagues)
        {
            List<DotaLeague> dotaLeagues = new List<DotaLeague>();
            foreach (var league in leagues)
            {
                DotaLeague dotaLeague = new DotaLeague(league);
                dotaLeagues.Add(dotaLeague);
            }

            return dotaLeagues;
        }
    }
}