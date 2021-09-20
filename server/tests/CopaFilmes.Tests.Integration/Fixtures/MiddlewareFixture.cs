using CopaFilmes.Api.Servicos;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Tests.Integration.Fixtures
{
    public class MiddlewareFixture : BaseFixture
    {
        public IUsuarioServico UsuarioServicoFake;

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            UsuarioServicoFake = A.Fake<IUsuarioServico>();
            services.AddScoped(_ => UsuarioServicoFake);
        }
    }
}
