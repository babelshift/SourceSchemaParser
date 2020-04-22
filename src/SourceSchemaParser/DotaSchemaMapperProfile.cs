using AutoMapper;
using SourceSchemaParser.DOTA2;
using Steam.Models.DOTA2;

namespace SourceSchemaParser
{
    public class DotaSchemaMapperProfile : Profile
    {
        public DotaSchemaMapperProfile()
        {
            CreateMap<DotaHeroSchemaItem, HeroSchemaModel>();
            CreateMap<DotaSchema, Steam.Models.DOTA2.SchemaModel>();
            CreateMap<DotaSchemaGameInfo, SchemaGameInfoModel>();
            CreateMap<DotaSchemaRarity, SchemaRarityModel>();
            CreateMap<DotaSchemaColor, SchemaColorModel>();
            CreateMap<DotaSchemaItem, Steam.Models.DOTA2.SchemaItemModel>();
            CreateMap<DotaSchemaPrefab, SchemaPrefabModel>();
            CreateMap<DotaSchemaItemSet, Steam.Models.DOTA2.SchemaItemSetModel>();
            CreateMap<DotaSchemaPrefab, SchemaPrefabModel>();
            CreateMap<DotaSchemaItemAutograph, SchemaItemAutographModel>();
            CreateMap<DotaSchemaQuality, SchemaQualityModel>();
            CreateMap<DotaSchemaPrefabCapability, SchemaPrefabCapabilityModel>();
            CreateMap<DotaSchemaItemTool, SchemaItemToolModel>();
            CreateMap<DotaSchemaItemToolUsage, SchemaItemToolUsageModel>();
            CreateMap<DotaSchemaItemPriceInfo, SchemaItemPriceInfoModel>();
            CreateMap<DotaItemAbilitySchemaItem, ItemAbilitySchemaItemModel>();
            CreateMap<DotaAbilitySpecialSchemaItem, AbilitySpecialSchemaItemModel>();
            CreateMap<DotaAbilitySchemaItem, AbilitySchemaItemModel>();
            CreateMap<DotaItemBuildSchemaItem, ItemBuildSchemaItemModel>();
            CreateMap<DotaItemBuildGroupSchemaItem, ItemBuildGroupSchemaItemModel>();
            CreateMap<DotaLeague, LeagueModel>();
        }
    }
}