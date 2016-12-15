using AutoMapper;
using SourceSchemaParser.DOTA2;
using Steam.Models.DOTA2;
using Steam.Models.GameEconomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser
{
    internal static class AutoMapperConfiguration
    {
        private static MapperConfiguration config;
        private static IMapper mapper;

        public static IMapper Mapper { get { return mapper; } }

        public static void Initialize()
        {
            if (config == null)
            {
                config = new MapperConfiguration(x =>
                {
                    x.CreateMap<DotaHeroSchemaItem, HeroSchemaModel>();

                    x.CreateMap<DotaSchema, SchemaModel>();
                    x.CreateMap<DotaSchemaGameInfo, SchemaGameInfoModel>();
                    x.CreateMap<DotaSchemaRarity, SchemaRarityModel>();
                    x.CreateMap<DotaSchemaColor, SchemaColorModel>();
                    x.CreateMap<DotaSchemaItem, SchemaItemModel>();
                    x.CreateMap<DotaSchemaPrefab, SchemaPrefabModel>();
                    x.CreateMap<DotaSchemaItemSet, SchemaItemSetModel>();
                    x.CreateMap<DotaSchemaPrefab, SchemaPrefabModel>();
                    x.CreateMap<DotaSchemaItemAutograph, SchemaItemAutographModel>();
                    x.CreateMap<DotaSchemaQuality, SchemaQualityModel>();
                    x.CreateMap<DotaSchemaPrefabCapability, SchemaPrefabCapabilityModel>();
                    x.CreateMap<DotaSchemaItemTool, SchemaItemToolModel>();
                    x.CreateMap<DotaSchemaItemToolUsage, SchemaItemToolUsageModel>();
                    x.CreateMap<DotaSchemaItemPriceInfo, SchemaItemPriceInfoModel>();

                    x.CreateMap<DotaItemAbilitySchemaItem, ItemAbilitySchemaItemModel>();
                    x.CreateMap<DotaAbilitySpecialSchemaItem, AbilitySpecialSchemaItemModel>();

                    x.CreateMap<DotaAbilitySchemaItem, AbilitySchemaItemModel>();

                    x.CreateMap<DotaItemBuildSchemaItem, ItemBuildSchemaItemModel>();
                    x.CreateMap<DotaItemBuildGroupSchemaItem, ItemBuildGroupSchemaItemModel>();

                    x.CreateMap<DotaLeague, LeagueModel>();
                });
            }

            if (mapper == null)
            {
                mapper = config.CreateMapper();
            }
        }

        public static void Reset()
        {
            config = null;
            mapper = null;
        }
    }
}
