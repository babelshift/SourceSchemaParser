using AutoMapper;
using SourceSchemaParser.DOTA2;
using Steam.Models.DOTA2;

namespace SourceSchemaParser
{
    public class DotaSchemaMapperProfile : Profile
    {
        public DotaSchemaMapperProfile()
        {
            CreateMap<DotaHeroSchemaItem, HeroSchema>();
            CreateMap<DotaSchema, Schema>();
            CreateMap<DotaSchemaGameInfo, SchemaGameInfo>();
            CreateMap<DotaSchemaRarity, SchemaRarity>();
            CreateMap<DotaSchemaColor, SchemaColor>();
            CreateMap<DotaSchemaItem, SchemaItem>();
            CreateMap<DotaSchemaPrefab, SchemaPrefab>();
            CreateMap<DotaSchemaItemSet, SchemaItemSet>();
            CreateMap<DotaSchemaItemAutograph, SchemaItemAutograph>();
            CreateMap<DotaSchemaQuality, SchemaQuality>();
            CreateMap<DotaSchemaPrefabCapability, SchemaPrefabCapability>();
            CreateMap<DotaSchemaItemTool, SchemaItemTool>();
            CreateMap<DotaSchemaItemToolUsage, SchemaItemToolUsage>();
            CreateMap<DotaSchemaItemPriceInfo, SchemaItemPriceInfo>();
            CreateMap<DotaSchemaItemStaticAttribute, SchemaItemStaticAttribute>();
            CreateMap<DotaItemAbilitySchemaItem, ItemAbility>();
            CreateMap<DotaAbilitySpecialSchemaItem, HeroAbilitySpecial>();
            CreateMap<DotaAbilitySchemaItem, HeroAbility>();
            CreateMap<DotaItemBuildSchemaItem, ItemBuild>();
            CreateMap<DotaItemBuildGroupSchemaItem, ItemBuildGroup>();
            CreateMap<DotaLeague, LeagueModel>();
        }
    }
}