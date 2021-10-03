using CopaFilmes.Api.Util;
using CopaFilmes.Tests.Common.Builders;
using FluentAssertions;
using System;
using Xunit;

namespace CopaFilmes.Tests.Unit.Util
{
    public class DatabaseCommonTests
    {
        private const string CONNECTION = "host=localhost;port=5432;database=db;username=user;password=pwd;pooling=true";

        [Fact]
        public void ParseConnectionString_DeveRetornarConexão_QuandoStringComum()
        {
            //Arrange

            //Act
            var actual = DatabaseCommon.ParseConnectionString(CONNECTION);

            //Assert
            actual.Should().BeEquivalentTo(CONNECTION);
        }

        [Fact]
        public void ParseConnectionString_DeveRetornarConexão_QuandoStringUrl()
        {
            //Arrange
            var connectionString = "postgres://user:pwd@localhost:5432/db";

            //Act
            var actual = DatabaseCommon.ParseConnectionString(connectionString);

            //Assert
            actual.Should().BeEquivalentTo(CONNECTION);
        }

        [Fact]
        public void ParseConnectionString_DeveRetornarErro_QuandoStringInvalida()
        {
            //Arrange
            var connectionString = UtilFaker.FakerHub.Random.AlphaNumeric(10);

            //Act
            Action act = () => { DatabaseCommon.ParseConnectionString(connectionString); };

            //Assert
            act.Should().Throw<FormatException>().WithMessage("Invalid connection string!");
        }
    }
}
