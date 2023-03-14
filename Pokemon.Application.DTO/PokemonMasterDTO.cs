using System.ComponentModel.DataAnnotations;

namespace Pokemon.Application.DTO
{
    /// <summary>
    /// Representa um mestre Pokémon.
    /// </summary>
    public class PokemonMasterDTO
    {
        /// <summary>
        /// Obtém ou define o nome do mestre Pokémon.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define a idade do mestre Pokémon.
        /// </summary>
        [Required]
        public int Age { get; set; }

        /// <summary>
        /// Obtém ou define o CPF do mestre Pokémon.
        /// </summary>
        [Required]
        public string CPF { get; set; }

        /// <summary>
        /// Obtém ou define o ID do mestre Pokémon.
        /// </summary>
        public int Id { get; set; }
    }
}
