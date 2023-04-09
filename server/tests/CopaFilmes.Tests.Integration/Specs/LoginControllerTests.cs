using AutoBogus;
using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Servicos.Login;
using CopaFilmes.Tests.Common.Util;
using CopaFilmes.Tests.Integration.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Tests.Integration.Specs;

[Collection(nameof(ApiTestCollection))]
public class LoginControllerTests : IDisposable
{
	private readonly ApiFixture _apiFixture;
	private readonly HttpClient _httpClient;
	private readonly string _endpoint;

	public LoginControllerTests(ApiFixture apiFixture)
	{
		apiFixture.Initialize().GetAwaiter().GetResult();

		_apiFixture = apiFixture;
		_httpClient = _apiFixture.GetHttpClient();
		_endpoint = ConfigManagerIntegration.ConfigRunTests.EndpointLogin;
	}

	[Fact]
	public async Task Post_DeveRetornarOk_QuandoTokenForGeradoCorretamente()
	{
		//Arrange
		var expected = new
		{
			Autenticado = true,
			Mensagem = Messages.Login_S001
		};

		//Act
		var response = await _httpClient.PostAsync(_endpoint, _apiFixture.Login.AsHttpContent());

		//Assert
		response.Should().Be200Ok().And.BeAs(expected);
	}

	[Fact]
	public async Task Post_DeveRetornarNotFound_QuandoSenhaForIncorreta()
	{
		//Arrange
		var expected = new
		{
			Autenticado = false,
			Mensagem = Messages.Login_F001
		};
		var loginIncorreto = new AutoFaker<LoginRequest>().Generate();

		//Act
		var response = await _httpClient.PostAsync(_endpoint, loginIncorreto.AsHttpContent());

		//Assert
		response.Should().Be404NotFound().And.BeAs(expected);
	}

	[Theory]
	[MemberData(nameof(GerarLoginIncorreto))]
	public async Task Post_DeveRetornarBadRequest_QuandoUsuarioOuSenhaNaoForemPreenchidos(LoginRequest loginIncorreto)
	{
		//Arrange

		//Act
		var response = await _httpClient.PostAsync(_endpoint, loginIncorreto.AsHttpContent());

		//Act
		response.Should().Be400BadRequest();
	}

	public static IEnumerable<object[]> GerarLoginIncorreto()
	{
		yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Usuario, string.Empty).Generate() };
		yield return new object[] { new AutoFaker<LoginRequest>().RuleFor(l => l.Senha, string.Empty).Generate() };
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing) => _httpClient.Dispose();
}
