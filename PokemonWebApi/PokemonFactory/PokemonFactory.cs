using Newtonsoft.Json.Linq;
using PokemonWebApi.PokeApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonWebApi.PokemonFactory
{
    public class PokemonFactory: IPokemonFactory
    {
        private readonly IPokeApiClient _pokeApiClient;

        public PokemonFactory(IPokeApiClient pokeApiClient)
        {
            _pokeApiClient = pokeApiClient;
        }

        public async Task<PokemonInfo> FetchPokemonAsync(string name)
        {
            string speciesJson;
            try
            {
                speciesJson = await _pokeApiClient.GetPokemonSpeciesAsync(name); // TODO: Pokemon name or species name?
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw; // This should produce a 500
            }

            return MapPokemonInfo(name, speciesJson);
        }


        private static PokemonInfo MapPokemonInfo(string name, string speciesJson)
        {
            JObject species = JObject.Parse(speciesJson);
            string habitat = (string)species["habitat"]["name"];
            string flavorText = (string)species["flavor_text_entries"].Where(x => (string)x["language"]["name"] == "en").First()["flavor_text"];
            var description = CleanFlavorText(flavorText);
            bool isLegendary = (bool)species["is_legendary"];
            return new PokemonInfo
            {
                Name = name,
                Description = description,
                Habitat = habitat,
                IsLegendary = isLegendary
            };
        }

        private static string CleanFlavorText(string flavorText)
        {
            return flavorText.Replace('\n', ' ').Replace('\f', ' ');
        }
    }
}
