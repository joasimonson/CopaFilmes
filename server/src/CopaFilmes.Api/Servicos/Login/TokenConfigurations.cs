namespace CopaFilmes.Api.Servicos.Login
{
    internal sealed class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}