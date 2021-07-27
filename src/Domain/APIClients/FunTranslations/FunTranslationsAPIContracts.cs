using Newtonsoft.Json;
namespace Domain.ApiClients
{
    internal class TranslationResponse
    {
        [JsonProperty("success")]
        public Success Success { get; set; }

        [JsonProperty("contents")]
        public Contents Contents { get; set; }
    }

    internal class Contents
    {
        [JsonProperty("translated")]
        public string Translated { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("translation")]
        public string Translation { get; set; }
    }

    internal class Success
    {
        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public class FunTranslateAPIOptions
    {
        public const string FunTranslateAPI = "FunTranslateAPI";

        public string URL { get; set; }
        public string queryParam { get; set; }

        public FunTranslateAPIEndpointsOptions[] Endpoints { get; set; }
    }

    public class FunTranslateAPIEndpointsOptions
    {
        public string Resource { get; set; }
        public string[] SupportedHabitats { get; set; }
        public bool IsLegendarySupported { get; set; }
    }
}
