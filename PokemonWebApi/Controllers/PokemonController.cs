using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PokemonWebApi.PokemonFactory;

namespace PokemonWebApi.Controllers
{
    [ApiController]
    [Route("pokemon/{name}")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonFactory _pokemonFactory;

        public PokemonController(ILogger<PokemonController> logger, IPokemonFactory pokemonFactory)
        {
            _logger = logger;
            _pokemonFactory = pokemonFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PokemonInfo>> GetAsync(string name)
        {
            var pokemon = await _pokemonFactory.FetchPokemonAsync(name);
            if (pokemon == null)
                return NotFound();
            return pokemon;
        }
    }
}
