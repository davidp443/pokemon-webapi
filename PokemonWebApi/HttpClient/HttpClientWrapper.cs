using System.Threading.Tasks;

namespace PokemonWebApi.HttpClient
{
    public class HttpClientWrapper : IHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient = new();

        public async Task<string> GetStringAsync(string requestUri)
        {
            return await _httpClient.GetStringAsync(requestUri);
        }
    }
}
