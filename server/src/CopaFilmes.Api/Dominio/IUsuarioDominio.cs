using System.Threading.Tasks;

namespace CopaFilmes.Api.Dominio
{
    public interface IUsuarioDominio
    {
        Task<bool> CriarAsync(string usuario, string senha);
        bool Existe(string usuario, string senha);
    }
}
