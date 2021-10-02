using CopaFilmes.Api.Util;
using FluentAssertions;
using System;
using Xunit;

namespace CopaFilmes.Tests.Unit.Util
{
    public class SegurancaCommonTests
    {
        [Fact]
        public void Criptografar_DeveRetornarErro_QuandoTextoForMenorQuePermitido()
        {
            //Arrange
            var textoInvalido = string.Empty;

            //Act
            Action act = () => { SegurancaCommon.Criptografar(textoInvalido); };

            //Assert
            act.Should().Throw<ArgumentException>().WithMessage("Value does not fall within the expected range. (Parameter 'text')");
        }

        [Fact]
        public void Criptografar_DeveRetornarTextoCriptografado()
        {
            //Arrange
            var textoValido = "awp9@sd&(&A";
            var cripografado = "672784197d165e7a2dca81d56bcc53532f2824c1d4cfa27e7fac9942f20f6b94";

            //Act
            var actual = SegurancaCommon.Criptografar(textoValido);

            //Assert
            actual.Should().Be(cripografado);
        }
    }
}
