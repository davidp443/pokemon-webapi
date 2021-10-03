using System.Threading.Tasks;

namespace PokemonWebApi.FunTranslationsClient
{
    public interface IFunTranslationsClient
    {
        Task<string> TranslateAsync(string translationType, string rawDescription);
    }
}
