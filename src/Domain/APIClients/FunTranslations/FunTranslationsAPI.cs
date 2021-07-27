using System;
using Domain.DataContracts;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Linq;
using Newtonsoft.Json;
using System.Web;

namespace Domain.ApiClients
{
    public class FunTranslationsAPI : ITranslateAPIClient
    {
        private readonly FunTranslateAPIOptions _options;
        private readonly HttpClient _httpClient;

        public FunTranslationsAPI(HttpClient httpClient, IOptions<FunTranslateAPIOptions> options)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }
        public async Task<string> Get(PokemonDetail pokemon)
        {
            var url = BuildURL(pokemon);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TranslationResponse>(responseBody);
            return result.Contents.Translated;

        }

        private string BuildURL(PokemonDetail pokemon)
        {
            var endpoint = _options.Endpoints
                                   .SingleOrDefault(x => x.SupportedHabitats?.Contains(pokemon.Habitat) == true
                                                      || x.IsLegendarySupported == pokemon.IsLegendary);

            return $"{_options.URL}{endpoint.Resource}?{_options.queryParam}={pokemon.Description}";
        }
    }
}
