namespace Pokemon.Application.DTO
{
    /// <summary>
    /// Representa uma cadeia de evolução de um Pokémon em resposta a uma solicitação feita à API pokeapi.co.
    /// </summary>
    public class EvolutionChainDTO
    {
        /// <summary>
        /// Obtém ou define o elo inicial da cadeia de evolução deste Pokémon.
        /// </summary>
        public EvolutionChainLinkDTO Chain { get; set; }
    }

    /// <summary>
    /// Representa um elo da cadeia de evolução de um Pokémon em resposta a uma solicitação feita à API pokeapi.co.
    /// </summary>
    public class EvolutionChainLinkDTO
    {
        /// <summary>
        /// Obtém ou define os elos de evolução que este elo pode evoluir.
        /// </summary>
        public EvolutionChainLinkDTO[] Evolves_To { get; set; }

        /// <summary>
        /// Obtém ou define a espécie do Pokémon deste elo da cadeia de evolução.
        /// </summary>
        public PokemonSpeciesDTO Species { get; set; }
    }

    /// <summary>
    /// Representa a espécie de um Pokémon em resposta a uma solicitação feita à API pokeapi.co.
    /// </summary>
    public class PokemonSpeciesDTO
    {
        /// <summary>
        /// Obtém ou define o nome da espécie do Pokémon.
        /// </summary>
        public string Name { get; set; }
    }
}
