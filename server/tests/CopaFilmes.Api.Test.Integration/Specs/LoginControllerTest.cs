using AutoBogus;
using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Test.Common.Util;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Api.Test.Integration.Specs
{
    public class LoginControllerTest : BaseFixture
    {
        internal readonly ILoginServico LoginServicoFake;
        internal readonly LoginRequest Login;

        public LoginControllerTest()
        {
            LoginServicoFake = A.Fake<ILoginServico>();

            Login = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, Configuration.GetSection("AccessKey").Value).Generate();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            //services.AddScoped(_ => LoginServicoFake);
        }

        [Fact]
        public async Task Post_DeveRetornarOk_QuandoTokenForGeradoCorretamente()
        {
            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, Login.AsHttpContent());

            response
                .Should()
                .Be200Ok()
                .And
                .Satisfy<LoginResult>(r =>
                    r.Autenticado = true
                    && r.Mensagem == Messages.Login_S001
                );
        }

        [Fact]
        public async Task Post_DeveretornarInternalServerError_QuandoHouverFalhaNaGeracaoDoTokenDeAcesso()
        {
            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, Login.AsHttpContent());

            response.Should().Be500InternalServerError();
        }

        [Fact]
        public async Task Post_DeveRetornarNotFound_QuandoSenhaForIncorreta()
        {
            var loginIncorreto = new AutoFaker<LoginRequest>().Generate();

            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, loginIncorreto.AsHttpContent());

            response
                .Should()
                .Be404NotFound()
                .And
                .Satisfy<LoginResult>(r =>
                    r.Autenticado = false
                    && r.Mensagem == Messages.Login_F001
                );
        }

        [Theory]
        [MemberData(nameof(GerarLoginIncorreto))]
        public async Task Post_DeveRetornarBadRequest_QuandoUsuarioOuSenhaNaoForemPreenchidos(LoginRequest loginIncorreto)
        {
            var response = await HttpClient.PostAsync(ConfigRunTests.EndpointLogin, loginIncorreto.AsHttpContent());

            response.Should().Be400BadRequest();
        }

        public static IEnumerable<object[]> GerarLoginIncorreto()
        {
            yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Usuario, string.Empty).Generate() };
            yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, string.Empty).Generate() };
        }
    }
}
