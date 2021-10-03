using PokemonWebApi.HttpClient;
using System;
using System.Threading.Tasks;

namespace PokemonWebApi.PokeApiClient
{
    public class PokeApiClient: IPokeApiClient
    {
        private readonly IHttpClient _httpClient;
        
        public PokeApiClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPokemonSpeciesAsync(string name)
        {
            return  await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon-species/" + Uri.EscapeDataString(name));
        }
    }
}
