using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Campeonato;
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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class CampeonatoController : ControllerBase
    {
        private readonly ILogger<CampeonatoController> _logger;
        private readonly ICampeonatoServico _campeonatoServico;

        public CampeonatoController(ILogger<CampeonatoController> logger, ICampeonatoServico campeonatoServico)
        {
            _logger = logger;
            _campeonatoServico = campeonatoServico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilmePosicaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FilmePosicaoModel>>> Post([FromBody] IEnumerable<CampeonatoRequest> campeonato)
        {
            try
            {
                var ranking = await _campeonatoServico.Disputar(campeonato);
                return Ok(ranking);
            }
            catch (RegraException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
