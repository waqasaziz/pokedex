
using Domain.DataContracts;
using System.Threading.Tasks;

namespace Domain.ApiClients
{
    public interface ITranslateAPIClient
    {
        Task<string> Get(PokemonDetail pokemon);
    }
}
