using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataContracts
{
    public class SearchResult
    {
        public PokemonDetail Pokemon { get; set; }
        public SearchResultStatus Status { get; set; } = SearchResultStatus.Found;
    }
}
