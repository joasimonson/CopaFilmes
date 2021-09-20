using CopaFilmes.Tests.Integration.Fixtures;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class UsuarioControllerTests
    {
        private readonly ApiFixture _apiFixture;
        private readonly DatabaseFixture _databaseFixture;

        public UsuarioControllerTests(ApiFixture apiFixture, DatabaseFixture databaseFixture)
        {
            _apiFixture = apiFixture;
            _databaseFixture = databaseFixture;
        }
    }
}
