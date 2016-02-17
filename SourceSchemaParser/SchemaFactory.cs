using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SourceSchemaParser.JsonConverters;
using SourceSchemaParser.VDFTools;
using SourceSchemaParser.Dota2;
using Steam.Models.DOTA2;

namespace SourceSchemaParser
{
    public static class SchemaFactory
    {
        static SchemaFactory()
        {
            AutoMapperConfiguration.Initialize();
        }

        public static SchemaModel GetDotaSchema(string[] vdf)
        {
            string json = VDFConverter.ToJson(vdf);
            var schemaContainer = JsonConvert.DeserializeObject<DotaSchemaContainer>(json);
            var schemaModel = AutoMapperConfiguration.Mapper.Map<DotaSchema, SchemaModel>(schemaContainer.Schema);
            return schemaModel;
        }

        public static IReadOnlyCollection<ItemAbilitySchemaItemModel> GetDotaItemAbilities(string[] vdf)
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
                var itemAbilityModels = AutoMapperConfiguration.Mapper.Map<IList<DotaItemAbilitySchemaItem>, IList<ItemAbilitySchemaItemModel>>(itemAbilities);
                return new ReadOnlyCollection<ItemAbilitySchemaItemModel>(itemAbilityModels);
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

        public static IReadOnlyCollection<AbilitySchemaItemModel> GetDotaHeroAbilities(string[] vdf)
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
                var abilityModels = AutoMapperConfiguration.Mapper.Map<IList<DotaAbilitySchemaItem>, IList<AbilitySchemaItemModel>>(abilities);
                return new ReadOnlyCollection<AbilitySchemaItemModel>(abilityModels);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes Abilities schema file.");
            }
        }

        public static IReadOnlyCollection<HeroSchemaModel> GetDotaHeroes(string[] vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("vdf");
            }

            if(vdf.Length == 0)
            {
                return null;
            }

            JObject schema = ConvertVdfToJObject(vdf);

            JToken item = null;
            if (schema.TryGetValue("DOTAHeroes", out item))
            {
                var heroes = JsonConvert.DeserializeObject<IList<DotaHeroSchemaItem>>(item.ToString(), new SchemaItemToDotaHeroJsonConverter());
                var heroModels = AutoMapperConfiguration.Mapper.Map<IList<DotaHeroSchemaItem>, IList<HeroSchemaModel>>(heroes);
                return new ReadOnlyCollection<HeroSchemaModel>(heroModels);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes schema file.");
            }
        }

        public static ItemBuildSchemaItemModel GetDotaItemBuild(string[] vdf)
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
                var itemBuildModel = AutoMapperConfiguration.Mapper.Map<DotaItemBuildSchemaItem, ItemBuildSchemaItemModel>(itemBuild);
                return itemBuildModel;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Build schema file.");
            }
        }

        #endregion

        #region Dota Leagues

        public static IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromText(string[] itemSchemaVdfText, string[] localizationVdfText)
        {
            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);

            var leagues = GetDotaLeagues(leaguesFromSchema, localizationVdfText);
            var leagueModels = AutoMapperConfiguration.Mapper.Map<IReadOnlyCollection<DotaLeague>, IReadOnlyCollection<LeagueModel>>(leagues);

            return leagueModels;
        }

        public static IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromFile(string itemSchemaFilePath, string localizationFilePath)
        {
            var itemSchemaVdfText = File.ReadAllLines(itemSchemaFilePath);
            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);

            var localizationVdfText = File.ReadAllLines(localizationFilePath);

            var leagues = GetDotaLeagues(leaguesFromSchema, localizationVdfText);
            var leagueModels = AutoMapperConfiguration.Mapper.Map<IReadOnlyCollection<DotaLeague>, IReadOnlyCollection<LeagueModel>>(leagues);

            return leagueModels;
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
                league.ItemName = GetLanguageToken(league.ItemName.Remove(0, 1), tokens);
                league.ItemDescription = GetLanguageToken(league.ItemDescription.Remove(0, 1), tokens);
                if (!String.IsNullOrEmpty(league.ItemTypeName))
                {
                    league.ItemTypeName = GetLanguageToken(league.ItemTypeName.Remove(0, 1), tokens);
                }
                else
                {
                    league.ItemTypeName = "Unknown";
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

        public static IReadOnlyCollection<SchemaPrefabModel> GetDotaItemPrefabs(string[] vdf)
        {
            string json = VDFConverter.ToJson(vdf);

            JObject itemSchema = JObject.Parse(json);

            JToken item = itemSchema["items_game"]["prefabs"];

            var prefabs = JsonConvert.DeserializeObject<IList<DotaSchemaPrefab>>(item.ToString(), new DotaSchemaPrefabJsonConverter());
            var prefabModels = AutoMapperConfiguration.Mapper.Map<IList<DotaSchemaPrefab>, IList<SchemaPrefabModel>>(prefabs);

            return new ReadOnlyCollection<SchemaPrefabModel>(prefabModels);
        }
    }
}