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
        public static DotaSchema GetDotaSchema(string[] vdf)
        {
            string json = VDFConverter.ToJson(vdf);
            var schema = JsonConvert.DeserializeObject<DotaSchemaContainer>(json);
            return schema.Schema;
        }

        public static IReadOnlyCollection<DotaItemAbilitySchemaItem> GetDotaItemAbilities(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = ConvertVdfToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("DOTAAbilities", out item))
            {
                var itemAbilities = JsonConvert.DeserializeObject<IList<DotaItemAbilitySchemaItem>>(item.ToString(), new SchemaItemToDotaItemAbilityJsonConverter());
                return new ReadOnlyCollection<DotaItemAbilitySchemaItem>(itemAbilities);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Items Abilities schema file.");
            }
        }
        
        public static IReadOnlyDictionary<string, string> GetDotaPanoramaLocalizationKeys(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            var keys = GetLanguageTokensFromPanoramaSchema(vdf);

            return new ReadOnlyDictionary<string, string>(keys);
        }

        public static IReadOnlyDictionary<string, string> GetDotaPublicLocalizationKeys(string[] vdf)
        {
            if(vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            var keys = GetLanguageTokensFromLanguageSchema(vdf);

            return new ReadOnlyDictionary<string, string>(keys);
        }

        #region Dota Heroes

        public static IReadOnlyCollection<DotaAbilitySchemaItem> GetDotaHeroAbilities(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = ConvertVdfToJObject(vdf);

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

        public static IReadOnlyCollection<DotaHeroSchemaItem> GetDotaHeroes(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = ConvertVdfToJObject(vdf);

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

        public static DotaItemBuildSchemaItem GetDotaItemBuild(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            JObject schema = ConvertVdfToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("itembuilds", out item))
            {
                var itemBuild = item.ToObject<DotaItemBuildSchemaItem>();
                return itemBuild;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Build schema file.");
            }
        }

        #endregion

        #region Dota Leagues

        public static IReadOnlyCollection<DotaLeague> GetDotaLeaguesFromText(string[] itemSchemaVdfText, string[] localizationVdfText)
        {
            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);

            return GetDotaLeagues(leaguesFromSchema, localizationVdfText);
        }

        public static IReadOnlyCollection<DotaLeague> GetDotaLeaguesFromFile(string itemSchemaFilePath, string localizationFilePath)
        {
            var itemSchemaVdfText = File.ReadAllLines(itemSchemaFilePath);
            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);

            var localizationVdfText = File.ReadAllLines(localizationFilePath);

            return GetDotaLeagues(leaguesFromSchema, localizationVdfText);
        }

        #region Merge Parsed Leagues and Language Tokens

        private static IReadOnlyCollection<DotaLeague> GetDotaLeagues(IReadOnlyCollection<DotaSchemaItem> parsedDotaLeagues, string[] localizationVdfText)
        {
            var tokens = GetLanguageTokensFromLanguageSchema(localizationVdfText);
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

        private static IReadOnlyCollection<DotaSchemaItem> GetLeaguesFromItemSchema(string[] vdfText)
        {
            string json = VDFConverter.ToJson(vdfText);

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
        
        private static JObject ConvertVdfToJObject(string[] vdf)
        {
            string json = VDFConverter.ToJson(vdf);
            JObject parsedSchema = JObject.Parse(json);
            return parsedSchema;
        }

        private static IDictionary<string, string> GetLanguageTokensFromPanoramaSchema(string[] localizationVdfText)
        {
            var json = VDFConverter.ToJson(localizationVdfText);
            return GetLanguageTokensFromPanoramaJson(json);
        }

        private static IDictionary<string, string> GetLanguageTokensFromPanoramaJson(string json)
        {
            JObject languageSchema = JObject.Parse(json);

            JToken item = null;
            if (languageSchema.TryGetValue("dota", out item))
            {
                var tokens = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.ToString(), new SchemaLanguageTokensJsonConverter());

                return tokens;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Panorama Localization schema file.");
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

        private static IDictionary<string, string> GetLanguageTokensFromLanguageSchema(string[] localizationVdfText)
        {
            var json = VDFConverter.ToJson(localizationVdfText);
            return GetLanguageTokensFromLanguageJson(json);
        }

        private static IDictionary<string, string> GetLanguageTokensFromLanguageJson(string json)
        {
            JObject languageSchema = JObject.Parse(json);

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

        public static IReadOnlyCollection<DotaSchemaPrefab> GetDotaItemPrefabs(string[] vdf)
        {
            string json = VDFConverter.ToJson(vdf);

            JObject itemSchema = JObject.Parse(json);

            JToken item = itemSchema["items_game"]["prefabs"];

            var prefabs = JsonConvert.DeserializeObject<IList<DotaSchemaPrefab>>(item.ToString(), new DotaSchemaPrefabJsonConverter());

            return new ReadOnlyCollection<DotaSchemaPrefab>(prefabs);
        }
    }
}