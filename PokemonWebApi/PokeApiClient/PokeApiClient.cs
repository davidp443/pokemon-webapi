using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonWebApi.PokeApiClient
{
    public class PokeApiClient
    {
        private readonly HttpClient _httpClient = new();

        public async Task<string> GetPokemonSpeciesAsync(string name)
        {
            var json = await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon-species/" + Uri.EscapeDataString(name));
            return json;
        }
    }
}
