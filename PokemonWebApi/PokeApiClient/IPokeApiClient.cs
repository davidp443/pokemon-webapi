using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi.PokeApiClient
{
    public interface IPokeApiClient
    {
         Task<string> GetPokemonSpeciesAsync(string name);
    }
}
