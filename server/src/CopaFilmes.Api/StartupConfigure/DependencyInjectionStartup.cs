using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Dominio.Filme;
using CopaFilmes.Api.Dominio.Usuario;
using CopaFilmes.Api.Middlewares.Exceptions;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Campeonato;
using CopaFilmes.Api.Servicos.Filme;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Servicos.Usuario;
using CopaFilmes.Api.Util;
using CopaFilmes.Api.Wrappers.MemoryCache;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class DependencyInjectionStartup
    {
        public static void Configurar(IServiceCollection services)
        {
            services.AddScoped<ICampeonatoDominio, CampeonatoDominio>();
            services.AddScoped<IFilmeDominio, FilmeDominio>();
            services.AddScoped<IUsuarioDominio, UsuarioDominio>();

            services.AddScoped<ICampeonatoServico, CampeonatoServico>();
            services.AddScoped<IFilmeServico, FilmeServico>();
            services.AddScoped<ILoginServico, LoginServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();

            services.AddScoped<TokenManager>();

            services.AddTransient<IApplicationModelProvider, ProduceResponseTypeModelProvider>();

            services.AddMemoryCache();
            services.AddSingleton<MemoryCacheWrapper>();

            services.AddControllers();
        }
    }
}
