namespace CopaFilmes.Api.Settings;

public sealed class SystemSettings
{
	public int MaximoParticipantesCampeonato { get; set; }
	public int MemoryCacheMinutesExpire { get; set; }
	public string FilmesCacheKey { get; set; }
	public string UrlWeb { get; set; }
}
