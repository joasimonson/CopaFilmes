using CopaFilmes.Api.Extensions;
using Microsoft.Extensions.Configuration;

namespace CopaFilmes.Tests.Integration
{
    internal static class ConfigManagerIntegration
    {
        internal static readonly IConfiguration TestConfiguration = new ConfigurationBuilder().AddJsonFile($"appsettings.{EnvironmentsExtensions.Test}.json").Build();

        internal static readonly ConfigRunTests ConfigRunTests = TestConfiguration.GetSettings<ConfigRunTests>();
        internal static readonly string TestConnectionString = TestConfiguration.GetConnectionString("TestConnection");
    }
}
