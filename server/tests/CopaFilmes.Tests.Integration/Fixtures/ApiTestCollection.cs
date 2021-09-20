using Xunit;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    [CollectionDefinition(nameof(ApiTestCollection), DisableParallelization = true)]
    public class ApiTestCollection
        : ICollectionFixture<ApiFixture>,
        ICollectionFixture<MiddlewareFixture>,
        ICollectionFixture<ApiFilmesFixture>,
        ICollectionFixture<DatabaseFixture>
    {
    }
}
