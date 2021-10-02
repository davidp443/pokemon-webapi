using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PokemonWebApi.PokemonFactory;

namespace PokemonWebApi.Controllers
{
    [ApiController]
    [Route("pokemon/translated/{name}")]
    public class PokemonTranslatedController : ControllerBase
    {
        private readonly ILogger<PokemonTranslatedController> _logger;
        private readonly IPokemonFactory _pokemonFactory;

        public PokemonTranslatedController(ILogger<PokemonTranslatedController> logger, IPokemonFactory pokemonFactory)
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
