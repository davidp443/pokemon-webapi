using System.Threading.Tasks;

namespace PokemonWebApi.HttpClient
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string requestUri);
    }
}
