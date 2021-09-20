using AutoBogus;
using CopaFilmes.Api.Middlewares.Exceptions;
using CopaFilmes.Api.Servicos.Usuario;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Fixtures;
using FakeItEasy;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs
{
    [Collection(nameof(ApiTestCollection))]
    public class ExceptionMiddlewareTests
    {
        private readonly MiddlewareFixture _apiFixture;
        private readonly UsuarioRequest _usuario;

        public ExceptionMiddlewareTests(MiddlewareFixture apiFixture)
        {
            _apiFixture = apiFixture;
            _usuario = new AutoFaker<UsuarioRequest>().Generate();

            A.CallTo(() => _apiFixture.UsuarioServicoFake
                .CriarAsync(A<UsuarioRequest>
                    .That
                    .Matches(u => u.Usuario == _usuario.Usuario && u.Senha == _usuario.Senha)))
                .ThrowsAsync(new Exception());
        }

        [Fact]
        public async Task DeveRetornarInternalServerError_QuandoHouverExceptionNoRequest()
        {
            //Arrange

            //Act
            var response = await _apiFixture.GetDefaultHttpClient().PostAsync(_apiFixture.ConfigRunTests.EndpointUsuario, _usuario.AsHttpContent());

            //Assert
            response.Should().Be500InternalServerError();
        }

        [Fact]
        public async Task DeveRetornarResponsePadrao_QuandoHouverExceptionNoRequest()
        {
            //Arrange

            //Act
            var response = await _apiFixture.GetDefaultHttpClient().PostAsync(_apiFixture.ConfigRunTests.EndpointUsuario, _usuario.AsHttpContent());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Action act = () => { JsonConvert.DeserializeObject<ExceptionResponse>(jsonResponse); };

            //Assert
            act.Should().NotThrow();
        }
    }
}
