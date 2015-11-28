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
        public static IReadOnlyCollection<DotaLeague> GetDotaLeagues(string schemaFilePath, string languageFilePath)
        {
            var vdfTextLines = File.ReadAllLines(schemaFilePath);

            var leagues = GetLeaguesFromSchema(vdfTextLines);

            var tokens = GetLanguageTokensFromLanguageSchema(languageFilePath);

            ReplaceTokensWithLocalizedValues(leagues, tokens);

            List<DotaLeague> dotaLeagues = FlattenDotaSchemaItemLeagues(leagues);

            return dotaLeagues;
        }

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

        private static void ReplaceTokensWithLocalizedValues(IReadOnlyCollection<DotaSchemaItem> leagues, IDictionary<string, string> tokens)
        {
            foreach (var league in leagues)
            {
                league.NameLocalized = GetToken(league.NameLocalized.Remove(0, 1), tokens);
                league.DescriptionLocalized = GetToken(league.DescriptionLocalized.Remove(0, 1), tokens);
                if (!String.IsNullOrEmpty(league.TypeName))
                {
                    league.TypeName = GetToken(league.TypeName.Remove(0, 1), tokens);
                }
                else
                {
                    league.TypeName = "Unknown";
                }
            }
        }

        private static string GetToken(string key, IDictionary<string, string> tokens)
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

        private static IReadOnlyCollection<DotaSchemaItem> GetLeaguesFromSchema(string[] vdfTextLines)
        {
            string json = VDFConverter.ToJson(vdfTextLines);

            JObject itemSchema = JObject.Parse(json);

            JToken item = itemSchema["items_game"]["items"];

            var leagues = JsonConvert.DeserializeObject<IList<DotaSchemaItem>>(item.ToString(), new SchemaItemsToDotaLeaguesJsonConverter());

            return new ReadOnlyCollection<DotaSchemaItem>(leagues);
        }
    }
}