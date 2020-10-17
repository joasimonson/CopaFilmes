using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Login
{
    public class LoginServico : ILoginServico
    {
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly string ACCESS_KEY;

        public LoginServico(IConfiguration configuration, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            ACCESS_KEY = configuration.GetValue<string>("AccessKey");

            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<LoginResult> AutenticarAsync(LoginRequest login)
        {
            if (String.IsNullOrWhiteSpace(login.Usuario) || String.IsNullOrWhiteSpace(login.Senha))
            {
                return Falha();
            }

            if (ACCESS_KEY == login.Senha)
            {
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromMinutes(_tokenConfigurations.Minutes);

                string token = await Task.FromResult(GenereteJwtToken(login.Usuario, createDate, expirationDate));

                return Sucesso(login.Usuario, token, createDate, expirationDate);
            }
            else
            {
                return Falha();
            }
        }

        private LoginResult Sucesso(string usuario, string token, DateTime createDate, DateTime expirationDate)
        {
            return new LoginResult
            {
                Autenticado = true,
                Criacao = createDate,
                Expiracao = expirationDate,
                Token = token,
                Usuario = usuario,
                Mensagem = "Login efetuado com sucesso"
            };
        }

        private LoginResult Falha()
        {
            return new LoginResult
            {
                Autenticado = false,
                Mensagem = "Falha ao autenticar"
            };
        }

        private string GenereteJwtToken(string usuario, DateTime createDate, DateTime expirationDate)
        {
            var genericIdentoty = new GenericIdentity(usuario);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario),
            };

            var claimsIdentity = new ClaimsIdentity(genericIdentoty, claims);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.Credentials,
                Subject = claimsIdentity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            string token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
