using CopaFilmes.Api.Resources;
using CopaFilmes.Api.Util;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Login
{
    internal class LoginServico : ILoginServico
    {
        private readonly TokenManager _tokenManager;
        private readonly IUsuarioServico _usuarioServico;

        public LoginServico(TokenManager tokenManager, IUsuarioServico usuarioServico)
        {
            _tokenManager = tokenManager;
            _usuarioServico = usuarioServico;
        }

        public async Task<LoginResult> AutenticarAsync(LoginRequest login)
        {
            if (string.IsNullOrWhiteSpace(login.Usuario) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return Falha();
            }

            if (_usuarioServico.Existe(login.Usuario, login.Senha))
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
