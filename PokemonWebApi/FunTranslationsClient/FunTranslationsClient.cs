using Newtonsoft.Json.Linq;
using PokemonWebApi.HttpClient;
using System;
using System.Threading.Tasks;

namespace PokemonWebApi.FunTranslationsClient
{
    public class FunTranslationsClient: IFunTranslationsClient
    {
        private readonly IHttpClient _httpClient;

        public FunTranslationsClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranslateAsync(string translationType, string text)
        {
            string uri = $"https://api.funtranslations.com/translate/{Uri.EscapeDataString(translationType)}.json?text={Uri.EscapeDataString(text)}";
            string json =await _httpClient.GetStringAsync(uri);
            JObject jObject = JObject.Parse(json);
            string translated = (string)jObject["contents"]["translated"];
            return translated;
        }
    }
}
