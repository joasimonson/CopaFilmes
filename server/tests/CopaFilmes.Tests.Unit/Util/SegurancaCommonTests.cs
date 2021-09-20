using CopaFilmes.Api.Util;
using CopaFilmes.Tests.Common.Builders;
using FluentAssertions;
using System;
using Xunit;

namespace CopaFilmes.Tests.Unit.Util
{
    public class SegurancaCommonTests
    {
        private const int MINIMO_CARACTERES_CRIPTOGRAFIA = 8;

        [Fact]
        public void Criptografar_DeveRetornarErro_QuandoTextoForMenorQuePermitido()
        {
            //Arrange
            var textoInvalido = UtilFaker.FakerHub.Random.AlphaNumeric(MINIMO_CARACTERES_CRIPTOGRAFIA - 1);

            //Act
            Action act = () => { SegurancaCommon.Criptografar(textoInvalido); };

            //Assert
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage($"*Mínimo de {MINIMO_CARACTERES_CRIPTOGRAFIA} caracteres para criptografia.*");
        }

        [Fact]
        public void Criptografar_DeveRetornarTextoCriptografado()
        {
            //Arrange
            var textoValido = "awp9@sd&(&A(S*";
            var cripografado = "kmkJ7TKhlrSf7BtWulcgbg==";

            //Act
            var actual = SegurancaCommon.Criptografar(textoValido);

            //Assert
            actual.Should().Be(cripografado);
        }
    }
}
