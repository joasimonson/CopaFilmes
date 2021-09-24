using CopaFilmes.Api;
using CopaFilmes.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class BaseFixture<TStartup> : IDisposable
        where TStartup : class
    {
        protected WebApplicationFactory<TStartup> Factory { get; }

        private static readonly IConfiguration _testConfiguration = new ConfigurationBuilder().AddJsonFile($"appsettings.{EnvironmentsExtensions.Test}.json").Build();
        public readonly ConfigRunTests ConfigRunTests = _testConfiguration.GetSettings<ConfigRunTests>();

        public TService GetService<TService>() => Factory.Services.GetService<TService>();
        public HttpClient GetHttpClient() => Factory.CreateClient();

        public BaseFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentsExtensions.Test);

            Factory = new WebApplicationFactory<TStartup>().WithWebHostBuilder(builder => ConfigureHostBuilder(builder));
        }

        protected internal void ConfigureHostBuilder(IWebHostBuilder builder)
        {
            builder
                .ConfigureAppConfiguration((context, builder) => ConfigureAppConfiguration(context, builder))
                .UseEnvironment(EnvironmentsExtensions.Test)
                .ConfigureTestServices(ConfigureTestServices);
        }

        protected internal void ConfigureAppConfiguration(WebHostBuilderContext _, IConfigurationBuilder builder)
        {
            var config = _testConfiguration.AsEnumerable().ToList();

            var testConnection = config.FirstOrDefault(c => c.Key == "ConnectionStrings:TestConnection");
            config.Add(new("ConnectionStrings:DefaultConnection", testConnection.Value));

            builder.AddInMemoryCollection(config);

            ConfigureAppConfiguration(builder);
        }

        protected virtual void ConfigureAppConfiguration(IConfigurationBuilder builder) { }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
            services.AddHttpClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => Factory?.Dispose();
    }

    public class BaseFixture : BaseFixture<Startup>
    {
    }
}
