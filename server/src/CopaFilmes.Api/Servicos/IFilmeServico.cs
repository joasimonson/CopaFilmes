using CopaFilmes.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos
{
    public interface IFilmeServico
    {
        public Task<IEnumerable<FilmeModel>> ObterFilmesAsync();
    }
}
