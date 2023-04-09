using CopaFilmes.Api.Servicos.Login;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos;

public interface ILoginServico
{
	Task<LoginResult> AutenticarAsync(LoginRequest login);
}
