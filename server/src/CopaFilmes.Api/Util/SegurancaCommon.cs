using System;
using System.Security.Cryptography;
using System.Text;

namespace CopaFilmes.Api.Util
{
    public static class SegurancaCommon
    {
        public static string Criptografar(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(null, nameof(text));
            }

            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder sBuilder = new();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
