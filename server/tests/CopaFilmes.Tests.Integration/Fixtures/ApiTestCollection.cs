using Xunit;

namespace CopaFilmes.Api.Test.Integration.Fixtures
{
    [CollectionDefinition(nameof(ApiTestCollection), DisableParallelization = true)]
    public class ApiTestCollection : ICollectionFixture<ApiTokenFixture>
    {
    }
}
