namespace CopaFilmes.Api.Settings
{
    public sealed class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}