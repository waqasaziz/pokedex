using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Domain.Services;
using Domain.ApiClients;
using System.Net.Http;

namespace WebAPI.Helpers
{
    public static class Extensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FunTranslateAPIOptions>(config.GetSection(FunTranslateAPIOptions.FunTranslateAPI));
            services.Configure<PokeAPIOptions>(config.GetSection(PokeAPIOptions.PokeAPI));

            services.AddSingleton<HttpClient>();
            services.AddSingleton<IPokemonAPIClient, PokeAPI>();
            services.AddSingleton<ITranslateAPIClient, FunTranslationsAPI>();
            services.AddSingleton<ISearchService, SearchService>();

        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Payment Gateway",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
            });
        }

    }
}
