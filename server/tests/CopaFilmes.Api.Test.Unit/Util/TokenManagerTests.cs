using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Test.Common.Builders;
using CopaFilmes.Api.Util;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Api.Test.Unit.Util
{
    public class TokenManagerTests : BaseTests
    {
        private readonly string _usuario;

        public TokenManagerTests()
        {
            _usuario = UtilFaker.FakerHub.Person.FirstName;
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_DeveCriarTokenParaUsuarioSolicitado()
        {
            var tokenManager = CriarTokenManager();
            var token = await tokenManager.GenerateJwtToken(_usuario);

            token.Usuario.Should().Be(_usuario);
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_DeveCriarTokenCorretamente()
        {
            var tokenManager = CriarTokenManager();
            var token = await tokenManager.GenerateJwtToken(_usuario);

            token.CodigoToken.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_DeveCriarTokenComIntervaloSolicitado()
        {
            var tokenManager = CriarTokenManager();

            var token = await tokenManager.GenerateJwtToken(_usuario);
            var timespan = token.Expiracao - token.Criacao;

            timespan.TotalMinutes.Should().Be(_tokenSettings.Minutes);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GenerateJwtTokenAsync_DeveRetornarFalhaAoTentarGerarTokenSemUsuario(string usuarioInvalido)
        {
            var tokenManager = CriarTokenManager();

            Func<Task> act = async () => await tokenManager.GenerateJwtToken(usuarioInvalido);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_DeveRetornarFalhaAoTentarGerarTokenComIntervaloDeValidadeInvalido()
        {
            _tokenSettings.Minutes = UtilFaker.FakerHub.Random.Int(int.MinValue, 0);
            var tokenManager = CriarTokenManager(_tokenSettings);

            Func<Task> act = async () => await tokenManager.GenerateJwtToken(_usuario);

            await act.Should().ThrowAsync<Exception>();
        }

        private TokenManager CriarTokenManager(TokenSettings tokenSettings = null)
        {
            if (tokenSettings is null)
            {
                tokenSettings = _tokenSettings;
            }
            var tokenManager = new TokenManager(_signingSettings, Options.Create(tokenSettings));
            return tokenManager;
        }
    }
}
