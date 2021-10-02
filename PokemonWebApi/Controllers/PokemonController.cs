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
    [Route("[controller]")]
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

            JObject species = JObject.Parse(speciesJson);

            string flavorText = (string)species["flavor_text_entries"].Where(x => (string)x["language"]["name"] == "en").First()["flavor_text"];

            return new PokemonInfo
            {
                Name = name,
                Description =flavorText,
                Habitat = (string)species["habitat"]["name"],
                IsLegendary = (bool)species["is_legendary"]
            };
        }
    }
}
