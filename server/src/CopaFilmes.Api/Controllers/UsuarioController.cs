using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioService;

        public UsuarioController(IUsuarioServico usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UsuarioResult), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] UsuarioRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Usuario) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return BadRequest();
            }

            var result = await _usuarioService.CriarAsync(login);

            if (result.Sucesso)
            {
                return Ok(result);
            }
            else
            {
                return UnprocessableEntity(result);
            }
        }
    }
}
