namespace Pokemon.Application.DTO
{
    public class CapturedPokemonDTO
    {
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public int MasterId { get; set; }

        public string PokemonName { get; set; }
        public string MasterName { get; set; }
        public float CaptureRate { get; set; }
    }
}
