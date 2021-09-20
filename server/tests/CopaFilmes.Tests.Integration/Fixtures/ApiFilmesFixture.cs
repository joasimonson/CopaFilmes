using CopaFilmes.Api.Wrappers.MemoryCache;
using FakeItEasy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class ApiFilmesFixture : ApiFixture
    {
        public MemoryCacheWrapper MemoryCacheWrapperFake;

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            IMemoryCache memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions() { }));
            MemoryCacheWrapperFake = A.Fake<MemoryCacheWrapper>(opt => opt.WithArgumentsForConstructor(new object[] { memoryCache }));
            services.AddSingleton(_ => memoryCache);
            services.AddSingleton(_ => MemoryCacheWrapperFake);
        }
    }
}
