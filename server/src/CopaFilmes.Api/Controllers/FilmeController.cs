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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
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
        [ProducesResponseType(typeof(IEnumerable<FilmeModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FilmeModel>>> Get()
        {
            try
            {
                var filmesResponse = await _memoryCache.GetOrCreateAsync(_systemSettings.FilmesCacheKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_systemSettings.MemoryCacheMinutesExpire);
                    entry.SetPriority(CacheItemPriority.High);

                    return await _filmeServico.ObterFilmesAsync();
                });

                return Ok(filmesResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
