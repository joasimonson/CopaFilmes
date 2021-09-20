using AutoBogus;
using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Usuario;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Unit.Dominio
{
    public class UsuarioServicoTests : BaseTests
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IUsuarioDominio _usuarioDominioFake;

        private readonly UsuarioRequest _usuario;

        public UsuarioServicoTests()
        {
            _usuarioDominioFake = A.Fake<IUsuarioDominio>();
            _usuarioServico = new UsuarioServico(_usuarioDominioFake);

            _usuario = new AutoFaker<UsuarioRequest>().Generate();
        }

        [Fact]
        public void Existe_DeveRetornarSucesso_QuandoExistirUsuario()
        {
            //Arrange
            A.CallTo(() => _usuarioDominioFake.Existe(_usuario.Usuario, _usuario.Senha)).Returns(true);

            //Act
            var result = _usuarioServico.Existe(_usuario.Usuario, _usuario.Senha);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Existe_DeveRetornarFalha_QuandoNaoExistirUsuario()
        {
            //Arrange
            A.CallTo(() => _usuarioDominioFake.Existe(_usuario.Usuario, _usuario.Senha)).Returns(false);

            //Act
            var result = _usuarioServico.Existe(_usuario.Usuario, _usuario.Senha);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarSucesso_QuandoIncluirUsuario()
        {
            //Arrange
            A.CallTo(() => _usuarioDominioFake.CriarAsync(_usuario.Usuario, _usuario.Senha));

            var expect = new UsuarioResult()
            {
                Sucesso = true,
                Usuario = _usuario.Usuario,
                Mensagem = Messages.Usuario_S001
            };

            //Act
            var result = await _usuarioServico.CriarAsync(_usuario);

            //Assert
            result.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public async Task CriarAsync_DeveRetornarFalha_QuandoUsuarioExistir()
        {
            //Arrange
            A.CallTo(() => _usuarioDominioFake.CriarAsync(_usuario.Usuario, _usuario.Senha)).Throws<Exception>();

            var expect = new UsuarioResult()
            {
                Sucesso = false,
                Usuario = _usuario.Usuario,
                Mensagem = Messages.Usuario_F002
            };

            //Act
            var result = await _usuarioServico.CriarAsync(_usuario);

            //Assert
            result.Should().BeEquivalentTo(expect);
        }

        [Theory]
        [MemberData(nameof(GerarUsuarioIncorreto))]
        public async Task CriarAsync_DeveRetornarErro_QuandoUsuarioOuSenhaNaoForemPreenchidos(UsuarioRequest usuarioIncorreto)
        {
            //Arrange

            //Act
            Func<Task> act = async () => await _usuarioServico.CriarAsync(usuarioIncorreto);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage(Messages.Usuario_F001);
        }

        public static IEnumerable<object[]> GerarUsuarioIncorreto()
        {
            yield return new object[] { new AutoFaker<UsuarioRequest>().RuleFor(l => l.Usuario, string.Empty).Generate() };
            yield return new object[] { new AutoFaker<UsuarioRequest>().RuleFor(l => l.Senha, string.Empty).Generate() };
        }
    }
}
