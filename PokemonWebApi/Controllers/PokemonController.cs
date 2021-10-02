using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public PokemonInfo Get(string pokemonName)
        {
            return new PokemonInfo();
        }
    }
}
