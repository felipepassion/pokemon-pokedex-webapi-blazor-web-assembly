namespace Pokemon.Application.DTO
{
    public class CapturedPokemonDTO
    {
        /// <summary>
        /// Identificador único para cada Pokémon capturado.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador único para cada Pokémon que foi capturado.
        /// </summary>
        public int PokemonId { get; set; }

        /// <summary>
        /// Identificador único para cada mestre que capturou um Pokémon.
        /// </summary>
        public int MasterId { get; set; }

        /// <summary>
        /// Nome do Pokémon capturado.
        /// </summary>
        public string PokemonName { get; set; }

        /// <summary>
        /// Nome do mestre que capturou o Pokémon.
        /// </summary>
        public string MasterName { get; set; }

        /// <summary>
        /// Taxa de captura do Pokémon em porcentagem.
        /// </summary>
        public float CaptureRate { get; set; }
    }
}
