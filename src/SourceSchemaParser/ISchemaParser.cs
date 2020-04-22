using System.Collections.Generic;
using Steam.Models.DOTA2;

namespace SourceSchemaParser
{
    public interface ISchemaParser
    {
        IReadOnlyCollection<AbilitySchemaItemModel> GetDotaHeroAbilities(IEnumerable<string> vdf);
        IReadOnlyCollection<HeroSchemaModel> GetDotaHeroes(IEnumerable<string> vdf);
        IReadOnlyCollection<ItemAbilitySchemaItemModel> GetDotaItemAbilities(IEnumerable<string> vdf);
        ItemBuildSchemaItemModel GetDotaItemBuild(IEnumerable<string> vdf);
        IReadOnlyCollection<SchemaPrefabModel> GetDotaItemPrefabs(IEnumerable<string> vdf);
        IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromFile(string itemSchemaFilePath, string localizationFilePath);
        IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromText(IEnumerable<string> itemSchemaVdfText, IEnumerable<string> localizationVdfText);
        IReadOnlyDictionary<string, string> GetDotaPanoramaLocalizationKeys(IEnumerable<string> vdf);
        IReadOnlyDictionary<string, string> GetDotaPublicLocalizationKeys(IEnumerable<string> vdf);
        SchemaModel GetDotaSchema(IEnumerable<string> vdf);
    }
}