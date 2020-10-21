using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace CopaFilmes.Api.Servicos.Login
{
    internal sealed class SigningConfigurations
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials Credentials { get; set; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}