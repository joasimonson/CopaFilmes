using CopaFilmes.Api.Dominio;
using CopaFilmes.Api.Resources;
using System;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Servicos.Usuario;

internal class UsuarioServico : IUsuarioServico
{
	private readonly IUsuarioDominio _usuarioDominio;

	public UsuarioServico(IUsuarioDominio usuarioDominio) => _usuarioDominio = usuarioDominio;

	public async Task<UsuarioResult> CriarAsync(UsuarioRequest usuario)
	{
		if (string.IsNullOrWhiteSpace(usuario.Usuario) || string.IsNullOrWhiteSpace(usuario.Senha))
			throw new ArgumentException(Messages.Usuario_F001);

		try
		{
			await _usuarioDominio.CriarAsync(usuario.Usuario, usuario.Senha);

			return new()
			{
				Sucesso = true,
				Usuario = usuario.Usuario,
				Mensagem = Messages.Usuario_S001
			};
		}
		catch
		{
			return new()
			{
				Sucesso = false,
				Usuario = usuario.Usuario,
				Mensagem = Messages.Usuario_F002
			};
		}
	}

	public bool Existe(string usuario, string senha) => _usuarioDominio.Existe(usuario, senha);
}
