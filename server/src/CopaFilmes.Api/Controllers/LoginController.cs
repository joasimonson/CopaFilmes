using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginServico _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginServico loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest login)
        {
            if (String.IsNullOrWhiteSpace(login.Usuario) || String.IsNullOrWhiteSpace(login.Senha))
            {
                return BadRequest();
            }

            try
            {
                var result = await _loginService.AutenticarAsync(login);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
