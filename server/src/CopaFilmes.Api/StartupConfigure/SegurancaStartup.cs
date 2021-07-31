using CopaFilmes.Api.Extensions;
using CopaFilmes.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class SegurancaStartup
    {
        public static void ConfigurarJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var signingSettings = new SigningSettings();
            services.AddSingleton(signingSettings);

            var tokenSettings = configuration.GetSettings<TokenSettings>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = signingSettings.Key,
                    ValidAudience = tokenSettings.Audience,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(opt =>
            {
                var builder = new AuthorizationPolicyBuilder();
                var authPolicy = builder
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                opt.AddPolicy(JwtBearerDefaults.AuthenticationScheme, authPolicy);
            });
        }
    }
}
