using System;

namespace CopaFilmes.Api;

[Serializable]
public class RegraException : Exception
{
	public RegraException(string message) : base(message)
	{
	}
}