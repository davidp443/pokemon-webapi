using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonWebApi.PokemonFactory
{
    public class PokemonTranslatedFactory : IPokemonFactory, IPokemonTranslatedFactory
    {
        private readonly IPokemonFactory _pokemonFactory;
        // TODO: Inject this so it can be mocked
        private readonly HttpClient _httpClient = new();

        public async Task<string> GetPokemonSpeciesAsync(string name)
        {
            return await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon-species/" + Uri.EscapeDataString(name));
        }

        public PokemonTranslatedFactory(IPokemonFactory pokemonFactory)
        {
            _pokemonFactory = pokemonFactory;
        }

        public async Task<PokemonInfo> FetchPokemonAsync(string name)
        {
            var pokemonInfo = await _pokemonFactory.FetchPokemonAsync(name);
            if (pokemonInfo == null) return null;
            return new PokemonInfo
            {
                Name = pokemonInfo.Name,
                Description = TranslateDescription(pokemonInfo.Description),
                Habitat = pokemonInfo.Habitat,
                IsLegendary = pokemonInfo.IsLegendary
            };

        }

        private string TranslateDescription(string rawDescription)
        {
            return "";
        }
    }
}
