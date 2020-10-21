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
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<FilmePosicaoModel>>> Post([FromBody] IEnumerable<CampeonatoRequest> campeonato)
        {
            IEnumerable<FilmePosicaoModel> ranking;

            try
            {
                ranking = await _campeonatoServico.Disputar(campeonato);
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

            return Ok(ranking);
        }
    }
}
