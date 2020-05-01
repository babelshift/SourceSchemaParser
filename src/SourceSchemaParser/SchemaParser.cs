using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using SourceSchemaParser.JsonConverters;
using SourceSchemaParser.Utilities;
using SourceSchemaParser.DOTA2;
using Steam.Models.DOTA2;
using System.Linq;
using AutoMapper;

namespace SourceSchemaParser
{
    public class SchemaParser : ISchemaParser
    {
        private readonly IVDFConvert vdfConvert;
        private readonly IMapper mapper;

        public SchemaParser(IVDFConvert vdfConvert, IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.vdfConvert = vdfConvert ?? throw new ArgumentNullException(nameof(vdfConvert));
        }

        #region Dota Main Item Schema

        public Schema GetDotaSchema(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            var schemaContainer = vdfConvert.DeserializeObject<DotaSchemaContainer>(vdf.ToList());
            return mapper.Map<DotaSchema, Schema>(schemaContainer.Schema);
        }

        public IReadOnlyCollection<ItemAbility> GetDotaItemAbilities(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            JObject schema = vdfConvert.ToJObject(vdf.ToList());

            JToken item = null;
            if (schema.TryGetValue("DOTAAbilities", out item))
            {
                var itemAbilities = JsonConvert.DeserializeObject<IList<DotaItemAbilitySchemaItem>>(item.ToString(), new SchemaItemToDotaItemAbilityJsonConverter());
                var itemAbilityModels = mapper.Map<IList<DotaItemAbilitySchemaItem>, IList<ItemAbility>>(itemAbilities);
                return new ReadOnlyCollection<ItemAbility>(itemAbilityModels);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Items Abilities schema file.");
            }
        }

        #endregion

        #region Dota Localization 

        public IReadOnlyDictionary<string, string> GetDotaPanoramaLocalizationKeys(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            return GetLanguageTokensFromPanoramaSchema(vdf);
        }

        public IReadOnlyDictionary<string, string> GetDotaPublicLocalizationKeys(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            return GetLanguageTokensFromLanguageSchema(vdf);
        }

        #endregion

        #region Dota Heroes

        public IReadOnlyCollection<HeroAbility> GetDotaHeroAbilities(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            JObject schema = vdfConvert.ToJObject(vdf.ToList());

            JToken item = null;
            if (schema.TryGetValue("DOTAAbilities", out item))
            {
                var abilities = JsonConvert.DeserializeObject<IList<DotaAbilitySchemaItem>>(item.ToString(), new SchemaItemToDotaAbilityJsonConverter());
                var abilityModels = mapper.Map<IList<DotaAbilitySchemaItem>, IList<HeroAbility>>(abilities);
                return new ReadOnlyCollection<HeroAbility>(abilityModels);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes Abilities schema file.");
            }
        }

        public IReadOnlyCollection<HeroSchema> GetDotaHeroes(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            JObject schema = vdfConvert.ToJObject(vdf.ToList());

            JToken item = null;
            if (schema.TryGetValue("DOTAHeroes", out item))
            {
                var heroes = JsonConvert.DeserializeObject<IList<DotaHeroSchemaItem>>(item.ToString(), new SchemaItemToDotaHeroJsonConverter());
                var heroModels = mapper.Map<IList<DotaHeroSchemaItem>, IList<HeroSchema>>(heroes);
                return new ReadOnlyCollection<HeroSchema>(heroModels);
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Heroes schema file.");
            }
        }

        public ItemBuild GetDotaItemBuild(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            JObject schema = vdfConvert.ToJObject(vdf.ToList());

            JToken item = null;
            if (schema.TryGetValue("itembuilds", out item))
            {
                var itemBuild = item.ToObject<DotaItemBuildSchemaItem>();
                var itemBuildModel = mapper.Map<DotaItemBuildSchemaItem, ItemBuild>(itemBuild);
                return itemBuildModel;
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Build schema file.");
            }
        }

        #endregion

        #region Dota Leagues

        public IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromText(IEnumerable<string> itemSchemaVdfText, IEnumerable<string> localizationVdfText)
        {
            ValidateInput(itemSchemaVdfText);
            ValidateInput(localizationVdfText);

            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);
            var leagues = GetDotaLeagues(leaguesFromSchema, localizationVdfText);
            return mapper.Map<IReadOnlyCollection<DotaLeague>, IReadOnlyCollection<LeagueModel>>(leagues);
        }

        public IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromFile(string itemSchemaFilePath, string localizationFilePath)
        {
            var itemSchemaVdfText = File.ReadAllLines(itemSchemaFilePath);
            var leaguesFromSchema = GetLeaguesFromItemSchema(itemSchemaVdfText);
            var localizationVdfText = File.ReadAllLines(localizationFilePath);
            var leagues = GetDotaLeagues(leaguesFromSchema, localizationVdfText);
            return mapper.Map<IReadOnlyCollection<DotaLeague>, IReadOnlyCollection<LeagueModel>>(leagues);
        }

        #region Merge Parsed Leagues and Language Tokens

        private IReadOnlyCollection<DotaLeague> GetDotaLeagues(IEnumerable<DotaSchemaItem> parsedDotaLeagues, IEnumerable<string> localizationVdfText)
        {
            var tokens = GetLanguageTokensFromLanguageSchema(localizationVdfText);
            ReplaceTokensWithLocalizedValues(parsedDotaLeagues, tokens);
            return FlattenDotaSchemaItemLeagues(parsedDotaLeagues.ToList());
        }

        private void ReplaceTokensWithLocalizedValues(IEnumerable<DotaSchemaItem> leagues, IReadOnlyDictionary<string, string> tokens)
        {
            if (leagues == null || tokens == null || tokens.Count == 0)
            {
                return;
            }

            foreach (var league in leagues)
            {
                if (!string.IsNullOrWhiteSpace(league.ItemName))
                {
                    league.ItemName = GetLanguageToken(league.ItemName.Remove(0, 1), tokens);
                }
                if (!string.IsNullOrWhiteSpace(league.ItemDescription))
                {
                    league.ItemDescription = GetLanguageToken(league.ItemDescription.Remove(0, 1), tokens);
                }
                if (!string.IsNullOrEmpty(league.ItemTypeName))
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

        private IEnumerable<DotaSchemaItem> GetLeaguesFromItemSchema(IEnumerable<string> vdfText)
        {
            string json = vdfConvert.ToJson(vdfText.ToList());

            return GetLeaguesFromJson(json);
        }

        private IEnumerable<DotaSchemaItem> GetLeaguesFromJson(string json)
        {
            JObject itemSchema = JObject.Parse(json);

            JToken item = itemSchema["items_game"]["items"];

            var leagues = JsonConvert.DeserializeObject<IList<DotaSchemaItem>>(item.ToString(), new SchemaItemsToDotaLeaguesJsonConverter());

            return new ReadOnlyCollection<DotaSchemaItem>(leagues);
        }

        #endregion

        private IReadOnlyCollection<DotaLeague> FlattenDotaSchemaItemLeagues(IReadOnlyCollection<DotaSchemaItem> leagues)
        {
            var dotaLeagues = leagues.Select(league => new DotaLeague()
            {
                ItemDef = league.DefIndex,
                Name = league.Name,
                ImageInventoryPath = league.ImageInventoryPath,
                ImageBannerPath = league.ImageBannerPath,
                NameLocalized = league.ItemName,
                DescriptionLocalized = league.ItemDescription,
                TypeName = league.ItemTypeName,
                TournamentUrl = league.TournamentUrl,
                Tier = league.Tool?.Usage?.Tier,
                Location = league.Tool?.Usage?.Location,
                LeagueId = league.Tool?.Usage?.LeagueId ?? 0
            });

            return new ReadOnlyCollection<DotaLeague>(dotaLeagues.ToList());
        }

        #endregion

        public IReadOnlyCollection<SchemaPrefab> GetDotaItemPrefabs(IEnumerable<string> vdf)
        {
            ValidateInput(vdf);

            JObject itemSchema = vdfConvert.ToJObject(vdf.ToList());

            JToken item = null;
            if (itemSchema.TryGetValue("items_game", out item))
            {
                if (item.HasValues && item["prefabs"] != null)
                {
                    item = item["prefabs"];

                    var prefabs = JsonConvert.DeserializeObject<IList<DotaSchemaPrefab>>(item.ToString(), new DotaSchemaPrefabJsonConverter());

                    var prefabModels = mapper.Map<IList<DotaSchemaPrefab>, IList<SchemaPrefab>>(prefabs);

                    return new ReadOnlyCollection<SchemaPrefab>(prefabModels);
                }
                else
                {
                    throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Prefabs schema file.");
                }
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Dota Item Prefabs schema file.");
            }
        }

        private IReadOnlyDictionary<string, string> GetLanguageTokensFromPanoramaSchema(IEnumerable<string> localizationVdfText)
        {
            ValidateInput(localizationVdfText);

            var json = vdfConvert.ToJson(localizationVdfText.ToList());

            return GetLanguageTokensFromPanoramaJson(json);
        }

        private IReadOnlyDictionary<string, string> GetLanguageTokensFromPanoramaJson(string json)
        {
            JObject languageSchema = JObject.Parse(json);

            JToken item = null;
            if (languageSchema.TryGetValue("dota", out item))
            {
                return JsonConvert.DeserializeObject<ReadOnlyDictionary<string, string>>(item.ToString(), new SchemaLanguageTokensJsonConverter());
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Panorama Localization schema file.");
            }
        }

        private string GetLanguageToken(string key, IReadOnlyDictionary<string, string> tokens)
        {
            string localizedValue = String.Empty;
            bool exists = tokens.TryGetValue(key, out localizedValue);
            return exists ? localizedValue : "Unknown";
        }

        private IReadOnlyDictionary<string, string> GetLanguageTokensFromLanguageSchema(IEnumerable<string> localizationVdfText)
        {
            ValidateInput(localizationVdfText);

            var json = vdfConvert.ToJson(localizationVdfText.ToList());

            return GetLanguageTokensFromLanguageJson(json);
        }

        private IReadOnlyDictionary<string, string> GetLanguageTokensFromLanguageJson(string json)
        {
            ValidateInput(json);

            JObject languageSchema = JObject.Parse(json);

            JToken langItem = null;
            JToken item = null;
            if (languageSchema.TryGetValue("lang", out langItem) && ((JObject)langItem).TryGetValue("Tokens", out item))
            {
                return JsonConvert.DeserializeObject<ReadOnlyDictionary<string, string>>(item.ToString(), new SchemaLanguageTokensJsonConverter());
            }
            else
            {
                throw new ArgumentException("You supplied a VDF file, but it wasn't the expected Public Localization schema file.");
            }
        }

        private void ValidateInput(IEnumerable<string> vdf)
        {
            if (vdf == null)
            {
                throw new ArgumentNullException("Input cannot be empty or null.", nameof(vdf));
            }

            if (vdf.Count() == 0)
            {
                throw new ArgumentException("Input cannot be empty or null.", nameof(vdf));
            }
        }

        private void ValidateInput(string text)
        {
            if (String.IsNullOrWhiteSpace(text.Trim()))
            {
                throw new ArgumentNullException("Input cannot be empty or null.", "text");
            }
        }
    }
}