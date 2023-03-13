using Newtonsoft.Json;

namespace Pokemon.Application.DTO
{
    public class PokemonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<string> Types { get; set; }

        [JsonProperty("evolution_chain")]
        public PokemonEvolutionChainLinkResponseDTO EvolutionChain { get; set; }
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
    }
}