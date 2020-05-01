using System.Collections.Generic;
using Steam.Models.DOTA2;

namespace SourceSchemaParser
{
    public interface ISchemaParser
    {
        IReadOnlyCollection<HeroAbility> GetDotaHeroAbilities(IEnumerable<string> vdf);
        IReadOnlyCollection<HeroSchema> GetDotaHeroes(IEnumerable<string> vdf);
        IReadOnlyCollection<ItemAbility> GetDotaItemAbilities(IEnumerable<string> vdf);
        ItemBuild GetDotaItemBuild(IEnumerable<string> vdf);
        IReadOnlyCollection<SchemaPrefab> GetDotaItemPrefabs(IEnumerable<string> vdf);
        IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromFile(string itemSchemaFilePath, string localizationFilePath);
        IReadOnlyCollection<LeagueModel> GetDotaLeaguesFromText(IEnumerable<string> itemSchemaVdfText, IEnumerable<string> localizationVdfText);
        IReadOnlyDictionary<string, string> GetDotaPanoramaLocalizationKeys(IEnumerable<string> vdf);
        IReadOnlyDictionary<string, string> GetDotaPublicLocalizationKeys(IEnumerable<string> vdf);
        Schema GetDotaSchema(IEnumerable<string> vdf);
    }
}