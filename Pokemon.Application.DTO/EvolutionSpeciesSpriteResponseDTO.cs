using Newtonsoft.Json;

namespace Pokemon.Application.DTO
{
    public class EvolutionSpeciesSpriteResponseDTO
    {
        [JsonProperty("default")]
        public string Default { get; set; }
    }

}
