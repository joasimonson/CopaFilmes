namespace CopaFilmes.Api.Servicos.Login
{
    public sealed class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}