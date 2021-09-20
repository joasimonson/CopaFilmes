using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CopaFilmes.Api.Util
{
    public static class SegurancaCommon
    {
        private const int MINIMO_CARACTERES_CRIPTOGRAFIA = 8;

        public static string Criptografar(string text)
        {
            if (text.Length < MINIMO_CARACTERES_CRIPTOGRAFIA)
            {
                throw new ArgumentOutOfRangeException(nameof(text), text.Length, $"Mínimo de {MINIMO_CARACTERES_CRIPTOGRAFIA} caracteres para criptografia.");
            }

            using var provider = new DESCryptoServiceProvider();

            var key = Encoding.UTF8.GetBytes(text.Substring(0, 8));
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, provider.CreateEncryptor(key, key), CryptoStreamMode.Write);
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            cs.Write(bytes, offset: 0, bytes.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
