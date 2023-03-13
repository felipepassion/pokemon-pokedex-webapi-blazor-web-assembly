namespace Pokemon.Application.DTO
{
    public class PokemonDTO
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<string> Types { get; set; }
        public string SpriteUrl { get; set; }
        public List<EvolutionDTO> Evolutions { get; set; }
    }
}