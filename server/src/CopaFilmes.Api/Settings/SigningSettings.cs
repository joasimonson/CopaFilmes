using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace CopaFilmes.Api.Settings;

internal sealed class SigningSettings
{
	public SecurityKey Key { get; set; }
	public SigningCredentials Credentials { get; set; }

	public SigningSettings()
	{
		using (var provider = new RSACryptoServiceProvider(2048))
		{
			Key = new RsaSecurityKey(provider.ExportParameters(true));
		}

		Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
	}
}