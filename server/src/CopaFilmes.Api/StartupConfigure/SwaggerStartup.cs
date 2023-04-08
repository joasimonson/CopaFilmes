using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class SwaggerStartup
    {
        public static void Configurar(IServiceCollection services)
        {
            services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Copa Filmes",
                    Description = "API para consulta e formação de disputa de filmes",
                    Contact = new OpenApiContact
                    {
                        Name = "Jô Araújo",
                        Url = new Uri("https://github.com/joasimonson/CopaFilmes")
                    }
                });
                opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Entre com o Token JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
