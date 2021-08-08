using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Util;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Login
{
    internal class LoginServico : ILoginServico
    {
        private readonly string ACCESS_KEY;
        private readonly TokenManager _tokenManager;

        public LoginServico(IConfiguration configuration, TokenManager tokenManager)
        {
            ACCESS_KEY = configuration.GetValue<string>("AccessKey");
            _tokenManager = tokenManager;
        }

        public async Task<LoginResult> AutenticarAsync(LoginRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Usuario) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return Falha();
            }

            if (ACCESS_KEY == login.Senha)
            {
                var token = await _tokenManager.GenerateJwtToken(login.Usuario);

                return Sucesso(token);
            }
            else
            {
                return Falha();
            }
        }

        private LoginResult Sucesso(Token token)
        {
            return new LoginResult
            {
                Autenticado = true,
                Criacao = token.Criacao,
                Expiracao = token.Expiracao,
                Token = token.CodigoToken,
                Usuario = token.Usuario,
                Mensagem = Messages.Login_S001
            };
        }

        private LoginResult Falha()
        {
            return new LoginResult
            {
                Autenticado = false,
                Mensagem = Messages.Login_F001
            };
        }
    }
}
