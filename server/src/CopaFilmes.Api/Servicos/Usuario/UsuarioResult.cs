using Newtonsoft.Json;

namespace CopaFilmes.Api.Servicos.Usuario
{
    public sealed class UsuarioResult
    {
        [JsonIgnore]
        public bool Sucesso { get; set; }
        public string Usuario { get; set; }
        public string Mensagem { get; set; }
    }
}
