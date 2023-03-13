using Newtonsoft.Json;

namespace Pokemon.Application.DTO
{
    public class EvolutionSpeciesResponseDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("capture_rate")]
        public float CaptureRate { get; set; }

        [JsonProperty("evolution_chain")]
        public EvolutionSpeciesChainItemDTO EvolutionChain { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class EvolutionSpeciesChainItemDTO
    {
        public string Url { get; set; }
    }
}
