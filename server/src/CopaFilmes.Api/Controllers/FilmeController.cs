using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class FilmeController : ControllerBase
    {
        private readonly ILogger<FilmeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IFilmeServico _filmeServico;
        private readonly SystemSettings _systemSettings;

        public FilmeController(ILogger<FilmeController> logger, IMemoryCache memoryCache, IOptions<SystemSettings> systemSettings, IFilmeServico filmeDominio)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _systemSettings = systemSettings.Value;
            _filmeServico = filmeDominio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeModel>>> Get()
        {
            IEnumerable<FilmeModel> filmesResponse;

            try
            {
                filmesResponse = await _memoryCache.GetOrCreateAsync(nameof(filmesResponse), async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_systemSettings.MemoryCacheMinutesExpire);
                    entry.SetPriority(CacheItemPriority.High);

                    return await _filmeServico.ObterFilmesAsync();
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            if (filmesResponse is null)
            {
                return NoContent();
            }

            return Ok(filmesResponse);
        }
    }
}
