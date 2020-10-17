using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CopaFilmes.Api.Externo
{
    public class Recursos : IRecursos
    {
        private readonly HttpClient _httpClient;

        public Recursos(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string uri) where T : class
        {
            var httpResponse = await _httpClient.GetAsync(uri);

            if (!httpResponse.IsSuccessStatusCode)
            {
                //TODO: Colocar caso de teste para esta validação
                throw new Exception();
            }

            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            if (String.IsNullOrWhiteSpace(jsonResponse))
            {
                //TODO: Colocar caso de teste para esta validação
                throw new Exception();
            }

            T content;

            try
            {
                var opt = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                
                content = JsonSerializer.Deserialize<T>(jsonResponse, opt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return content;
        }
    }
}
