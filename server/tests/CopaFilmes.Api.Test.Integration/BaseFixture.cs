using CopaFilmes.Api.Dominio.Campeonato;
using CopaFilmes.Api.Dominio.Filme;
using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Servicos.Campeonato;
using CopaFilmes.Api.Servicos.Filme;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Servicos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CopaFilmes.Api.Test.Integration
{
    public class BaseFixture<TStartup> : IDisposable
        where TStartup : class
    {
        protected WebApplicationFactory<TStartup> Factory { get; }

        public readonly IConfiguration Configuration;
        public readonly IServiceProvider Services;

        public BaseFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Development);

            Factory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(builder => builder
                    .UseEnvironment(Environments.Development)
                    .ConfigureTestServices(ConfigureTestServices));

            Services = Factory.Services;
            Configuration = Factory.Services.GetService<IConfiguration>();
        }

        protected virtual void ConfigureTestServices(IServiceCollection serviceCollection) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Factory?.Dispose();
        }
    }

    public class Fixture : BaseFixture<Startup>
    {
        protected override void ConfigureTestServices(IServiceCollection services)
        {
            services.AddSingleton<IFilmeDominio, FilmeDominio>();
            services.AddSingleton<ICampeonatoDominio, CampeonatoDominio>();

            services.AddSingleton<ILoginServico, LoginServico>();
            services.AddSingleton<ICampeonatoServico, CampeonatoServico>();
            services.AddSingleton<IFilmeServico, FilmeServico>();

            services.AddSingleton<Fixture>();
        }
    }
}
