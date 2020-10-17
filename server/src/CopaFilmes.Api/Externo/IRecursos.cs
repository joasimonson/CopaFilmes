using System.Threading.Tasks;

namespace CopaFilmes.Api.Externo
{
    public interface IRecursos
    {
        Task<T> GetAsync<T>(string uri) where T : class;
    }
}
