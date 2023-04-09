using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Servicos.Campeonato;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(JwtBearerDefaults.AuthenticationScheme)]
public class CampeonatoController : ControllerBase
{
	private readonly ICampeonatoServico _campeonatoServico;

	public CampeonatoController(ICampeonatoServico campeonatoServico) => _campeonatoServico = campeonatoServico;

	[HttpPost]
	[ProducesResponseType(typeof(FilmePosicaoModel), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<IEnumerable<FilmePosicaoModel>>> Post([FromBody] IEnumerable<CampeonatoRequest> campeonato)
	{
		try
		{
			var ranking = await _campeonatoServico.Disputar(campeonato);
			return Ok(ranking);
		}
		catch (RegraException ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
