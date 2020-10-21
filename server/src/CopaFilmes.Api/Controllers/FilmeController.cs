using CopaFilmes.Api.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CopaFilmes.Api.Servicos;

namespace CopaFilmes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class FilmeController : ControllerBase
    {
        private readonly ILogger<FilmeController> _logger;
        private readonly IFilmeServico _filmeServico;

        public FilmeController(ILogger<FilmeController> logger, IFilmeServico filmeDominio)
        {
            _logger = logger;
            _filmeServico = filmeDominio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeModel>>> Get()
        {
            IEnumerable<FilmeModel> filmes;

            try
            {
                filmes = await _filmeServico.ObterFilmesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            if (filmes is null)
            {
                return NoContent();
            }

            return Ok(filmes);
        }
    }
}
