using CopaFilmes.Api.Servicos.Usuario;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos;

public interface IUsuarioServico
{
	Task<UsuarioResult> CriarAsync(UsuarioRequest usuario);
	bool Existe(string usuario, string senha);
}
