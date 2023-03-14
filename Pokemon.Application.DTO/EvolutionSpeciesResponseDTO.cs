namespace Pokemon.Application.DTO
{
    public class EvolutionSpeciesResponseDTO
    {
        /// <summary>
        /// Identificador único para a espécie de evolução.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A taxa de captura da espécie de evolução em porcentagem.
        /// </summary>
        public float Capture_Rate { get; set; }

        /// <summary>
        /// A cadeia de evolução da espécie.
        /// </summary>
        public EvolutionSpeciesChainItemDTO Evolution_Chain { get; set; }

        /// <summary>
        /// O nome da espécie de evolução.
        /// </summary>
        public string Name { get; set; }
    }

    public class EvolutionSpeciesChainItemDTO
    {
        /// <summary>
        /// A URL da cadeia de evolução da espécie.
        /// </summary>
        public string Url { get; set; }
    }
}
