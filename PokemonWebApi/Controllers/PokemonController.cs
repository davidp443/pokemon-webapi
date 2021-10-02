using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PokemonWebApi.Controllers
{
    [ApiController]
    [Route("pokemon/{name}")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokeApiClient _pokeApiClient;

        public PokemonController(ILogger<PokemonController> logger, IPokeApiClient pokeApiClient)
        {
            _logger = logger;
            _pokeApiClient = pokeApiClient;
        }

        [HttpGet]
        public async Task<PokemonInfo> GetAsync(string name)
        {
            string speciesJson = await _pokeApiClient.GetPokemonSpeciesAsync(name); // TODO: Pokemon name or species name?

            return MapPokemonInfo(name, speciesJson);
        }

        private PokemonInfo MapPokemonInfo(string name, string speciesJson)
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

        private string CleanFlavorText(string flavorText)
        {
            return flavorText.Replace('\n', ' ').Replace('\f',' ');
        }
    }
}
