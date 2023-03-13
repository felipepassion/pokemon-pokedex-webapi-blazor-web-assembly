using System.ComponentModel.DataAnnotations;

namespace Pokemon.Application.DTO
{
    public class PokemonMasterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string CPF { get; set; }
        public int Id { get; set; }
    }
}
