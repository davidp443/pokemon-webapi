using PokemonWebApi.FunTranslationsClient;
using System;
using System.Threading.Tasks;

namespace PokemonWebApi.PokemonFactory
{
    public class PokemonTranslatedFactory : IPokemonFactory, IPokemonTranslatedFactory
    {
        private readonly IPokemonFactory _pokemonFactory;
        private readonly IFunTranslationsClient _funTranslationsClient;

        public PokemonTranslatedFactory(IPokemonFactory pokemonFactory, IFunTranslationsClient funTranslationsClient)
        {
            _pokemonFactory = pokemonFactory;
            _funTranslationsClient = funTranslationsClient;
        }

        public async Task<PokemonInfo> FetchPokemonAsync(string name)
        {
            var pokemonInfo = await _pokemonFactory.FetchPokemonAsync(name);
            if (pokemonInfo == null) return null;
            return new PokemonInfo
            {
                Name = pokemonInfo.Name,
                Description = await TranslateDescriptionAsync(pokemonInfo.Description, pokemonInfo.Habitat, pokemonInfo.IsLegendary),
                Habitat = pokemonInfo.Habitat,
                IsLegendary = pokemonInfo.IsLegendary
            };

        }

        private async Task<string> TranslateDescriptionAsync(string rawDescription, string habitat, bool isLegendary)
        {
            string translationType = (habitat == "cave" || isLegendary) ? "yoda" : "shakespeare";
            try
            {
                return await _funTranslationsClient.TranslateAsync(translationType, rawDescription);
            }
            catch (Exception)
            {
                //TODO: We should probably be logging something
                return rawDescription;
            }
        }
    }
}
