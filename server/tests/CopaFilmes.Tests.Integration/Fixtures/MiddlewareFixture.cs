﻿using AutoBogus;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Usuario;
using CopaFilmes.Tests.Common.Util;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CopaFilmes.Tests.Integration.Fixtures;

public class MiddlewareFixture : BaseFixture
{
	private readonly IUsuarioServico _usuarioServicoFake = A.Fake<IUsuarioServico>();
	private readonly UsuarioRequest _usuario;
	private readonly HttpClient _httpClient;

	public MiddlewareFixture()
	{
		_usuario = new AutoFaker<UsuarioRequest>().Generate();
		_httpClient = GetHttpClient();
		A.CallTo(() => _usuarioServicoFake
			.CriarAsync(A<UsuarioRequest>
				.That
				.Matches(u => u.Usuario == _usuario.Usuario && u.Senha == _usuario.Senha)))
			.ThrowsAsync(new Exception());
	}

	protected override void ConfigureTestServices(IServiceCollection services)
	{
		base.ConfigureTestServices(services);
		services.AddScoped(_ => _usuarioServicoFake);
	}

	public async Task<HttpResponseMessage> ExecuteMiddlewareAsync()
	{
		var response = await _httpClient.PostAsync(ConfigManagerIntegration.ConfigRunTests.EndpointUsuario, _usuario.AsHttpContent());

		return response;
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		_httpClient.Dispose();
	}
}
