using System.Text.Json.Serialization;

namespace CopaFilmes.Api.Model;

public class FilmeModel
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("titulo")]
	public string Titulo { get; set; }

	[JsonPropertyName("ano")]
	public int Ano { get; set; }

	[JsonPropertyName("nota")]
	public decimal Nota { get; set; }
}
