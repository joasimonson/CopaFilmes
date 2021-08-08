using CopaFilmes.Api.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Util
{
    internal sealed class TokenManager
    {
        private readonly SigningSettings _signingSettings;
        private readonly TokenSettings _tokenSettings;

        public TokenManager(SigningSettings signingSettings, IOptions<TokenSettings> tokenSettings)
        {
            _signingSettings = signingSettings;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<Token> GenerateJwtToken(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException(nameof(user));
            }

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromMinutes(_tokenSettings.Minutes);

            return await Task.FromResult(GenereteJwtToken(user, createDate, expirationDate));
        }

        private Token GenereteJwtToken(string user, DateTime createDate, DateTime expirationDate)
        {
            var genericIdentoty = new GenericIdentity(user);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user),
            };

            var claimsIdentity = new ClaimsIdentity(genericIdentoty, claims);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = _signingSettings.Credentials,
                Subject = claimsIdentity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            string token = handler.WriteToken(securityToken);

            return new Token()
            {
                Criacao = createDate,
                Expiracao = expirationDate,
                CodigoToken = token,
                Usuario = user,
            };
        }
    }

    internal sealed class Token
    {
        public string Usuario { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime Expiracao { get; set; }
        public string CodigoToken { get; set; }
    }
}
