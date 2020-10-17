using CopaFilmes.Api.Entidades;
using CopaFilmes.Api.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class FilmeController : ControllerBase
    {
        private readonly ILogger<FilmeController> _logger;
        private readonly IFilmeRepositorio _filmeRepositorio;

        public FilmeController(ILogger<FilmeController> logger, IFilmeRepositorio filmeRepositorio)
        {
            _logger = logger;
            _filmeRepositorio = filmeRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> Get()
        {
            IEnumerable<Filme> filmes;

            try
            {
                filmes = await _filmeRepositorio.ObterFilmesAsync();
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
