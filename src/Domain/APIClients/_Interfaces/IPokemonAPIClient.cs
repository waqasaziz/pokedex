using Domain.DataContracts;
using System.Threading.Tasks;

namespace Domain.ApiClients
{
    public interface IPokemonAPIClient
    {
        Task<PokemonDetail> Get(string name);
    }
}
