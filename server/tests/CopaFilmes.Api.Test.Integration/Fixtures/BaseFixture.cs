using CopaFilmes.Api.Extensions;
using Flurl.Http.Testing;
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

        internal readonly IConfiguration Configuration;
        internal readonly IServiceProvider Services;
        internal readonly HttpClient HttpClient;
        internal readonly ConfigRunTests ConfigRunTests;
        internal readonly HttpTest HttpTest;

        public BaseFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Development);

            Factory = new WebApplicationFactory<TStartup>()
                .WithWebHostBuilder(builder => builder
                    .UseEnvironment(Environments.Development)
                    .ConfigureTestServices(ConfigureTestServices));

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            ConfigRunTests = config.GetSettings<ConfigRunTests>();
            Services = Factory.Services;
            Configuration = Factory.Services.GetService<IConfiguration>();
            HttpClient = Factory.CreateClient();
            HttpTest = new HttpTest();
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
