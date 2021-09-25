using CopaFilmes.Api;
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
    public class BaseFixture : BaseFixture<Startup> { }
    public class BaseFixture<TStartup> : IDisposable
        where TStartup : class
    {
        protected WebApplicationFactory<TStartup> Factory { get; }

        public TService GetService<TService>() => Factory.Services.GetService<TService>();
        public HttpClient GetHttpClient() => Factory.CreateClient();

        public BaseFixture()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentsExtensions.Test);

            Factory = new WebApplicationFactory<TStartup>().WithWebHostBuilder(builder => ConfigureHostBuilder(builder));
        }

        private void ConfigureHostBuilder(IWebHostBuilder builder)
        {
            builder
                .ConfigureAppConfiguration((context, builder) => ConfigureAppConfiguration(context, builder))
                .UseEnvironment(EnvironmentsExtensions.Test)
                .ConfigureTestServices(ConfigureTestServices);
        }

        private void ConfigureAppConfiguration(WebHostBuilderContext _, IConfigurationBuilder builder)
        {
            var config = ConfigManagerIntegration.TestConfiguration.AsEnumerable().ToList();

            config.Add(new("ConnectionStrings:DefaultConnection", ConfigManagerIntegration.TestConnectionString));

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
}
