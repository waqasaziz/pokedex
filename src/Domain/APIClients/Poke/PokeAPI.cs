using System;
using System.Threading.Tasks;
using PokeApiNet;
using System.Linq;
using Domain.DataContracts;
using Microsoft.Extensions.Options;

namespace Domain.ApiClients
{

    public class PokeAPI : IPokemonAPIClient
    {

        private readonly PokeApiClient _pokeClient;
        private readonly PokeAPIOptions _options;

        public PokeAPI(PokeApiClient pokeClient, IOptions<PokeAPIOptions> options)
        {
            _options = options.Value;
            _pokeClient = pokeClient;
        }

        public async Task<PokemonDetail> Get(string name)
        {
            var pokemon = await _pokeClient.GetResourceAsync<Pokemon>(name);
            var species = await _pokeClient.GetResourceAsync(pokemon.Species);
            var desc = species.FlavorTextEntries
                              .FirstOrDefault(x => x.Language.Name == _options.DescLanguage)
                              ?.FlavorText
                              .Replace("\n", " ")
                              .Replace("\f", " ");

            return new PokemonDetail
            {
                Name = species.Name,
                Description = desc,
                Habitat = species.Habitat.Name,
                IsLegendary = species.IsLegendary
            };
        }

    }
}
