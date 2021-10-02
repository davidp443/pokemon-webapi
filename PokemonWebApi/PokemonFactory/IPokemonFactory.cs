using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi.PokemonFactory
{
    public interface IPokemonFactory
    {
        Task<PokemonInfo> FetchPokemonAsync(string name);
    }
}
