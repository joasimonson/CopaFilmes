using CopaFilmes.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace CopaFilmes.Api.Test.Integration.Fixtures
{
    public class BaseFixture<TStartup> : IDisposable
        where TStartup : class
    {
        protected WebApplicationFactory<TStartup> Factory { get; }

        public readonly IConfiguration Configuration;
        public readonly IServiceProvider Services;
        public readonly HttpClient HttpClient;
        internal readonly ConfigRunTests ConfigRunTests;

        public IConfiguration GetTestConfiguration() => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public virtual HttpClient CreateClient() => Factory.CreateClient();

        public BaseFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Development);

            Factory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(builder => builder
                    .UseEnvironment(Environments.Development)
                    .ConfigureTestServices(ConfigureTestServices));

            var config = GetTestConfiguration();

            ConfigRunTests = config.GetSettings<ConfigRunTests>();
            Services = Factory.Services;
            Configuration = Factory.Services.GetService<IConfiguration>();
            HttpClient = CreateClient();
        }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Factory?.Dispose();
            HttpClient.Dispose();
        }
    }

    public class BaseFixture : BaseFixture<Startup>
    {
    }
}
