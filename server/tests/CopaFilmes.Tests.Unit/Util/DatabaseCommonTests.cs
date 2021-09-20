using CopaFilmes.Api.Util;
using CopaFilmes.Tests.Common.Builders;
using FluentAssertions;
using System;
using Xunit;

namespace CopaFilmes.Tests.Unit.Util
{
    public class DatabaseCommonTests
    {
        public const string CONN = "host=localhost;port=5432;database=copafilmes;username=postgres;password=1234;pooling=true";

        [Fact]
        public void ParseConnectionString_DeveRetornarConexão_QuandoStringComum()
        {
            //Arrange

            //Act
            var actual = DatabaseCommon.ParseConnectionString(CONN);

            //Assert
            actual.Should().BeEquivalentTo(CONN);
        }

        [Fact]
        public void ParseConnectionString_DeveRetornarConexão_QuandoStringUrl()
        {
            //Arrange
            var connectionString = "postgres://postgres:1234@localhost:5432/CopaFilmes";

            //Act
            var actual = DatabaseCommon.ParseConnectionString(connectionString);

            //Assert
            actual.Should().BeEquivalentTo(CONN);
        }

        [Fact]
        public void ParseConnectionString_DeveRetornarErro_QuandoStringInvalida()
        {
            //Arrange
            var connectionString = UtilFaker.FakerHub.Random.AlphaNumeric(10);

            //Act
            Action act = () => { DatabaseCommon.ParseConnectionString(connectionString); };

            //Assert
            act.Should().Throw<FormatException>().WithMessage("String connection invalid!");
        }
    }
}
