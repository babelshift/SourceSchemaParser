using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AutoMapper;

namespace SourceSchemaParser.Utilities
{
    public static class SchemaParserServiceCollectionExtensions
    {
        public static IServiceCollection AddSourceSchemaParser(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<IVDFConvert, VDFConvert>());
            services.TryAdd(ServiceDescriptor.Singleton<ISchemaParser, SchemaParser>());
            services.AddAutoMapper(typeof(SchemaParser).Assembly);

            return services;
        }
    }
}