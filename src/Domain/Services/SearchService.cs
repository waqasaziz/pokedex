using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Domain.DataContracts;
using Domain.ApiClients;
using System.Runtime.CompilerServices;

namespace Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> _log;
        private readonly IPokemonAPIClient _pokeApiClient;
        private readonly ITranslateAPIClient _tanslateApiClient;

        public SearchService(
            ILogger<SearchService> log,
            IPokemonAPIClient pokeApiClient,
            ITranslateAPIClient tanslateApiClient)
        {
            _log = log;
            _pokeApiClient = pokeApiClient;
            _tanslateApiClient = tanslateApiClient;
        }

        public async Task<SearchResult> FindPokemon(string name, bool shouldTranslate = false)
        {
            var result = await GetPokemon(name);

            if (result.Status == SearchResultStatus.Found && shouldTranslate)
                result.Pokemon.Description = await TryTranslateDescription(result.Pokemon);

            return result;
        }

        private async Task<string> TryTranslateDescription(PokemonDetail pokemonDetail)
        {
            try
            {
                return await _tanslateApiClient.Get(pokemonDetail);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
            }

            return pokemonDetail.Description;
        }

        private async Task<SearchResult> GetPokemon(string name)
        {
            var result = new SearchResult();

            try
            {
                result.Pokemon = await _pokeApiClient.Get(name);
            }
            catch (HttpRequestException ex)
            {
                _log.LogError(ex, ex.Message);
                result.Status = SearchResultStatus.NotFound;
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                result.Status = SearchResultStatus.Error;
            }

            return result;
        }
    }
}
