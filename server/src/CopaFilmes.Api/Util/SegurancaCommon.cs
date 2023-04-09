using System;
using System.Security.Cryptography;
using System.Text;

namespace CopaFilmes.Api.Util;

public static class SegurancaCommon
{
	public static string Criptografar(string text)
	{
		if (string.IsNullOrWhiteSpace(text))
			throw new ArgumentException(null, nameof(text));

		byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(text));

		StringBuilder sBuilder = new();

		foreach (byte item in data)
		{
			sBuilder.Append(item.ToString("x2"));
		}

		return sBuilder.ToString();
	}
}
