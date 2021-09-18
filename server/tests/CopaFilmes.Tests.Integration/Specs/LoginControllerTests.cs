using AutoBogus;
using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Api.Settings;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs
{
    public class LoginControllerTests : BaseFixture
    {
        private readonly LoginRequest _login;
        private readonly HttpClient _client;

        public LoginControllerTests()
        {
            var accessKey = GetConfiguration().GetSection("AccessKey").Value;
            _login = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, accessKey).Generate();
            _client = GetDefaultHttpClient();
        }

        [Fact]
        public async Task Post_DeveRetornarOk_QuandoTokenForGeradoCorretamente()
        {
            var expected = new
            {
                Autenticado = true,
                Mensagem = Messages.Login_S001
            };
            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, _login.AsHttpContent());

            response.Should().Be200Ok().And.BeAs(expected);
        }

        [Fact]
        public async Task Post_DeveRetornarNotFound_QuandoSenhaForIncorreta()
        {
            var expected = new
            {
                Autenticado = false,
                Mensagem = Messages.Login_F001
            };
            var loginIncorreto = new AutoFaker<LoginRequest>().Generate();

            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, loginIncorreto.AsHttpContent());

            response.Should().Be404NotFound().And.BeAs(expected);
        }

        [Theory]
        [MemberData(nameof(GerarLoginIncorreto))]
        public async Task Post_DeveRetornarBadRequest_QuandoUsuarioOuSenhaNaoForemPreenchidos(LoginRequest loginIncorreto)
        {
            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, loginIncorreto.AsHttpContent());

            response.Should().Be400BadRequest();
        }

        public static IEnumerable<object[]> GerarLoginIncorreto()
        {
            yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Usuario, string.Empty).Generate() };
            yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, string.Empty).Generate() };
        }
    }

    public class LoginControllerTestWithMock : BaseFixture
    {
        private readonly LoginRequest _login;
        private readonly HttpClient _client;

        public LoginControllerTestWithMock()
        {
            var accessKey = GetConfiguration().GetSection("AccessKey").Value;
            _login = new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, accessKey).Generate();
            _client = GetDefaultHttpClient();
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            var settings = new TokenSettings();
            services.AddSingleton(Options.Create(settings));
        }

        [Fact]
        public async Task Post_DeveretornarInternalServerError_QuandoHouverFalhaNaGeracaoDoTokenDeAcesso()
        {
            var response = await _client.PostAsync(ConfigRunTests.EndpointLogin, _login.AsHttpContent());

            response.Should().Be500InternalServerError();
        }
    }
}
