using Newtonsoft.Json;

namespace Pokemon.Application.DTO
{
    public class EvolutionSpeciesResponseDTO
    {
        public int Id { get; set; }
        
        public float Capture_Rate { get; set; }

        public EvolutionSpeciesChainItemDTO Evolution_Chain { get; set; }

        public string Name { get; set; }
    }

    public class EvolutionSpeciesChainItemDTO
    {
        public string Url { get; set; }
    }
}
