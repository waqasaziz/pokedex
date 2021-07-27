using Domain.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISearchService
    {
        Task<SearchResult> FindPokemon(string name, bool shouldTranslate = false);
    }
}
