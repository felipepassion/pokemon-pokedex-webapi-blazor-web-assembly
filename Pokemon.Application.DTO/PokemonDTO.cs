namespace Pokemon.Application.DTO
{
    public class PokemonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<PokemonTypeDTO> Types { get; set; }
        public SpiecesDTO Spieces { get; set; }
        public PokemonEvolutionChainLinkResponseDTO Evolution_Chain { get; set; }
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
    }

    public class SpiecesDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonTypeDTO
    {
        public int Slot { get; set; }
        public PokemonTypeDetailDTO Type { get; set; }
    }

    public class PokemonTypeDetailDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}