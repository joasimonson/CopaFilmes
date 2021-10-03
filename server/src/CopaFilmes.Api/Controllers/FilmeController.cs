using CopaFilmes.Api.Model;
using CopaFilmes.Api.Servicos;
using CopaFilmes.Api.Settings;
using CopaFilmes.Api.Wrappers.MemoryCache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly MemoryCacheWrapper _memoryCache;
        private readonly IFilmeServico _filmeServico;
        private readonly SystemSettings _systemSettings;

        public FilmeController(MemoryCacheWrapper memoryCache, IOptions<SystemSettings> systemSettings, IFilmeServico filmeDominio)
        {
            _memoryCache = memoryCache;
            _systemSettings = systemSettings.Value;
            _filmeServico = filmeDominio;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FilmeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FilmeModel>>> Get()
        {
            var filmesResponse = await _memoryCache.GetOrCreateAsync(_systemSettings.FilmesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_systemSettings.MemoryCacheMinutesExpire);
                entry.SetPriority(CacheItemPriority.High);

                return await _filmeServico.ObterFilmesAsync();
            });

            return Ok(filmesResponse);
        }
    }
}
