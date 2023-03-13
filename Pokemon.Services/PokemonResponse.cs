namespace Pokemon.Api.Services
{
    using System.Collections.Generic;
    using Pokemon.Application.DTO;

    public class PokemonResponse
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<string> Types { get; set; }
        public PokemonEvolutionChainResponseDTO[] EvolutionChain { get; set; }
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";

    }
}