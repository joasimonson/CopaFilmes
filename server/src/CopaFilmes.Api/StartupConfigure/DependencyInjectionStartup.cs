using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Dominio.Filme;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Campeonato;
using CopaFilmes.Api.Servicos.Filme;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Util;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.StartupConfigure
{
    public static class DependencyInjectionStartup
    {
        public static void Configurar(IServiceCollection services)
        {
            services.AddScoped<IFilmeDominio, FilmeDominio>();
            services.AddScoped<ICampeonatoDominio, CampeonatoDominio>();

            services.AddScoped<ILoginServico, LoginServico>();
            services.AddScoped<ICampeonatoServico, CampeonatoServico>();
            services.AddScoped<IFilmeServico, FilmeServico>();

            services.AddScoped<TokenManager>();

            //services.AddTransient<IFlurlClient, FlurlClient>();

            services.AddControllers();
        }
    }
}
