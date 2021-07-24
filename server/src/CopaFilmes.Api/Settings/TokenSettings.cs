namespace CopaFilmes.Api.Settings
{
    internal sealed class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}