using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;

namespace CopaFilmes.Tests.Common.Util;

public static class Extensions
{
	public static StringContent AsHttpContent(this object content)
	{
		string jsonContent = JsonConvert.SerializeObject(content);
		return new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json);
	}
}
