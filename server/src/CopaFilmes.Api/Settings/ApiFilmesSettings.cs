namespace CopaFilmes.Api.Settings;

public sealed class ApiFilmesSettings
{
	public string Url { get; set; }
	public string EndpointFilmes { get; set; }
	public string URL_FILMES => Flurl.Url.Combine(Url, EndpointFilmes);
}
