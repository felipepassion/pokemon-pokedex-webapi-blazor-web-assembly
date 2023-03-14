namespace Pokemon.Application.DTO
{
    public class PokemonDTO
    {
        /// <summary>
        /// Identificador único do Pokémon.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do Pokémon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Altura do Pokémon em decímetros.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Peso do Pokémon em hectogramas.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Lista de tipos do Pokémon.
        /// </summary>
        public List<PokemonTypeDTO> Types { get; set; }

        /// <summary>
        /// Informações sobre a espécie do Pokémon.
        /// </summary>
        public SpeciesDTO Species { get; set; }

        /// <summary>
        /// Informações sobre a cadeia de evolução do Pokémon.
        /// </summary>
        public PokemonEvolutionChainLinkResponseDTO Evolution_Chain { get; set; }

        /// <summary>
        /// URL para a imagem do Pokémon.
        /// </summary>
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";

        /// <summary>
        /// Taxa de captura do Pokémon.
        /// </summary>
        public float Capture_Rate { get; set; }

        /// <summary>
        /// Informações sobre os sprites do Pokémon.
        /// </summary>
        public SpritesDTO Sprites { get; set; }

        /// <summary>
        /// Lista de informações sobre as evoluções do Pokémon.
        /// </summary>
        public List<EvolutionDTO> Evolutions { get; set; }
    }

    /// <summary>
    /// Classe que representa as informações de sprites de um Pokémon
    /// </summary>
    public class SpritesDTO
    {
        /// <summary>
        /// URL do sprite frontal padrão do Pokémon
        /// </summary>
        public string Front_Default { get; set; }
    }

    /// <summary>
    /// Classe que representa as informações de espécie de um Pokémon
    /// </summary>
    public class SpeciesDTO
    {
        /// <summary>
        /// Nome da espécie do Pokémon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL com mais informações sobre a espécie do Pokémon
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Classe que representa as informações de tipo de um Pokémon
    /// </summary>
    public class PokemonTypeDTO
    {
        /// <summary>
        /// Posição do tipo na lista de tipos do Pokémon
        /// </summary>
        public int Slot { get; set; }
        /// <summary>
        /// Tipo do Pokémon
        /// </summary>
        public PokemonTypeDetailDTO Type { get; set; }
    }

    /// <summary>
    /// Classe que representa as informações detalhadas de tipo de um Pokémon
    /// </summary>
    public class PokemonTypeDetailDTO
    {
        /// <summary>
        /// Nome do tipo do Pokémon
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// URL com mais informações sobre o tipo do Pokémon
        /// </summary>
        public string Url { get; set; }
    }
}