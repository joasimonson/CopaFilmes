using AutoBogus;
using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Servicos.Usuario;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Extensions;
using CopaFilmes.Tests.Integration.Fixtures;
using FluentAssertions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class UsuarioControllerTests
    {
        private readonly ApiFixture _apiFixture;
        private readonly HttpClient _client;
        private readonly string _endpoint;

        public UsuarioControllerTests(ApiFixture apiFixture)
        {
            apiFixture.Initializar().GetAwaiter().GetResult();

            _apiFixture = apiFixture;
            _client = _apiFixture.GetHttpClient();
            _endpoint = _apiFixture.ConfigRunTests.EndpointUsuario;
        }

        [Fact]
        public async Task Post_DeveRetornarOk_QuandoUsuarioCriadoComSucesso()
        {
            //Arrange
            var novoUsuario = new AutoFaker<UsuarioRequest>().Generate();
            var expected = new
            {
                novoUsuario.Usuario,
                Mensagem = Messages.Usuario_S001
            };

            //Act
            var response = await _client.PostAsync(_endpoint, novoUsuario.AsHttpContent());

            //Assert
            response.Should().Be200Ok().And.BeAs(expected);
        }

        [Fact]
        public async Task Post_DeveRetornarUnprocessableEntity_QuandoUsuarioJaEstiverCadastrado()
        {
            //Arrange
            var expected = new
            {
                _apiFixture.Usuario.Usuario,
                Mensagem = Messages.Usuario_F002
            };

            //Act
            var response = await _client.PostAsync(_endpoint, _apiFixture.Usuario.AsHttpContent());

            //Assert
            response.Should().Be422UnprocessableEntity().And.BeAs(expected);
        }

        [Theory]
        [MemberData(nameof(GerarUsuarioIncorreto))]
        public async Task Post_DeveRetornarBadRequest_QuandoUsuarioOuSenhaNaoForemPreenchidos(UsuarioRequest usuarioIncorreto)
        {
            //Arrange

            //Act
            var response = await _client.PostAsync(_endpoint, usuarioIncorreto.AsHttpContent());

            //Act
            response.Should().Be400BadRequest();
        }

        public static IEnumerable<object[]> GerarUsuarioIncorreto()
        {
            yield return new object[] { new AutoFaker<UsuarioRequest>().RuleFor(l => l.Usuario, string.Empty).Generate() };
            yield return new object[] { new AutoFaker<UsuarioRequest>().RuleFor(l => l.Senha, string.Empty).Generate() };
        }
    }
}
