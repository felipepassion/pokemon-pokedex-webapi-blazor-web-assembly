namespace Pokemon.Application.DTO
{
    public class PokemonEvolutionChainResponseDTO
    {
        public object BabyTriggerItem { get; set; }
        public EvolutionChain Chain { get; set; }
        public int Id { get; set; }
    }

    public class EvolutionChain
    {
        public List<EvolvesToDTO> EvolvesTo { get; set; }
        public bool IsBaby { get; set; }
        public SpeciesDTO Species { get; set; }
    }

    public class EvolvesToDTO
    {
        public List<EvolvesToDTO> EvolvesTo { get; set; }
        public SpeciesDTO Species { get; set; }
    }

    public class SpeciesDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
