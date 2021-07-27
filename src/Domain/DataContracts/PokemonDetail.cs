using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.DataContracts
{
    public class PokemonDetail
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("habitat")]
        public string Habitat { get; set; }

        [JsonPropertyName("isLegendary")]
        public bool IsLegendary { get; set; }

    }
}
