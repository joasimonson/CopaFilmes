using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
	private readonly ILoginServico _loginService;

	public LoginController(ILoginServico loginService) => _loginService = loginService;

	[AllowAnonymous]
	[HttpPost]
	[ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(LoginResult), StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Post([FromBody] LoginRequest login)
	{
		if (string.IsNullOrWhiteSpace(login.Usuario) || string.IsNullOrWhiteSpace(login.Senha))
		{
			return BadRequest();
		}

		var result = await _loginService.AutenticarAsync(login);

		return result is null || !result.Autenticado ? NotFound(result) : Ok(result);
	}
}
