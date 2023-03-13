namespace Pokemon.Application.DTO
{
    public class EvolutionChainDTO
    {
        public EvolutionChainLinkDTO Chain { get; set; }
    }

    public class EvolutionChainLinkDTO
    {
        public EvolutionDetailsDTO[] EvolutionDetails { get; set; }
        public EvolutionChainLinkDTO[] EvolvesTo { get; set; }
        public PokemonSpeciesDTO Species { get; set; }
    }

    public class EvolutionDetailsDTO
    {
        public int? MinLevel { get; set; }
    }

    public class PokemonSpeciesDTO
    {
        public string Name { get; set; }
    }
}
