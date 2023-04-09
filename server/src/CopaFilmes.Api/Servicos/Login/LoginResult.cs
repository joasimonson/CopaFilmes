using System;

namespace CopaFilmes.Api.Servicos.Login;

public sealed class LoginResult
{
	public bool Autenticado { get; set; }
	public DateTime Criacao { get; set; }
	public DateTime Expiracao { get; set; }
	public string Token { get; set; }
	public string Usuario { get; set; }
	public string Mensagem { get; set; }
}
