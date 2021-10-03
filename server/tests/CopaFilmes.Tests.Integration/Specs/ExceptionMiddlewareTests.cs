using CopaFilmes.Api.Middlewares.Exceptions;
using CopaFilmes.Tests.Integration.Fixtures;
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

        public ExceptionMiddlewareTests(MiddlewareFixture apiFixture)
        {
            _apiFixture = apiFixture;
        }

        [Fact]
        public async Task DeveRetornarInternalServerError_QuandoHouverExceptionNoRequest()
        {
            //Arrange

            //Act
            var response = await _apiFixture.ExecuteMiddlewareAsync();

            //Assert
            response.Should().Be500InternalServerError();
        }

        [Fact]
        public async Task DeveRetornarResponsePadrao_QuandoHouverExceptionNoRequest()
        {
            //Arrange

            //Act
            var response = await _apiFixture.ExecuteMiddlewareAsync();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Action act = () => { JsonConvert.DeserializeObject<ExceptionResponse>(jsonResponse); };

            //Assert
            act.Should().NotThrow();
        }
    }
}
