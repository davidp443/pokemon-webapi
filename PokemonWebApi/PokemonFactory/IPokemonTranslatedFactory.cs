using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonWebApi.PokemonFactory
{
    public interface IPokemonTranslatedFactory: IPokemonFactory // TODO: This interface is for DI purpose only. We should use IPokemonFactory instead
    { }
}
