using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SourceSchemaParser.JsonConverters;
using SourceSchemaParser.VDFTools;
using SourceSchemaParser.Dota2;

namespace SourceSchemaParser
{
    public static class SchemaFactory
    {
        public static DotaItemBuildSchemaItem GetDotaItemBuild(string vdf)
        {
            if (String.IsNullOrEmpty(vdf))
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = SchemaToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("itembuilds", out item))
            {
                var itemBuild = JsonConvert.DeserializeObject<DotaItemBuildSchemaItem>(item.ToString());
                return itemBuild;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Build schema file.");
            }
        }

        public static IReadOnlyDictionary<string, string> GetDotaPublicLocalizationKeys(string vdf)
        {
            if(String.IsNullOrEmpty(vdf))
            {
                throw new ArgumentNullException("vdf");
            }

            string json = VDFConverter.ToJson(vdf);

            var keys = GetLanguageTokensFromLanguageSchema(json);

            return new ReadOnlyDictionary<string, string>(keys);
        }

        #region Dota Heroes

        public static IReadOnlyCollection<DotaAbilitySchemaItem> GetDotaHeroAbilities(string vdf)
        {
            if (String.IsNullOrEmpty(vdf))
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = SchemaToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("DOTAAbilities", out item))
            {
                var abilities = JsonConvert.DeserializeObject<IList<DotaAbilitySchemaItem>>(item.ToString(), new SchemaItemToDotaAbilityJsonConverter());
                return new ReadOnlyCollection<DotaAbilitySchemaItem>(abilities);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes Abilities schema file.");
            }
        }

        private static JObject SchemaToJObject(string vdf)
        {
            string json = VDFConverter.ToJson(vdf);
            JObject parsedSchema = JObject.Parse(json);
            return parsedSchema;
        }

        public static IReadOnlyCollection<DotaHeroSchemaItem> GetDotaHeroes(string vdf)
        {
            if (String.IsNullOrEmpty(vdf))
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = SchemaToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("DOTAHeroes", out item))
            {
                var heroes = JsonConvert.DeserializeObject<IList<DotaHeroSchemaItem>>(item.ToString(), new SchemaItemToDotaHeroJsonConverter());
                return new ReadOnlyCollection<DotaHeroSchemaItem>(heroes);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes schema file.");
            }
        }

        #endregion

        #region Dota Leagues

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

        #endregion

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

        private static IDictionary<string, string> GetLanguageTokensFromLanguageSchema(string vdfText)
        {
            JObject languageSchema = JObject.Parse(vdfText);

            JToken langItem = null;
            JToken item = null;
            if (languageSchema.TryGetValue("lang", out langItem) && ((JObject)langItem).TryGetValue("Tokens", out item))
            {
                var tokens = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.ToString(), new SchemaLanguageTokensJsonConverter());

                return tokens;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Public Localization schema file.");
            }
        }
    }
}