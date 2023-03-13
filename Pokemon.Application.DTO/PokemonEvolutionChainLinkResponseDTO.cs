namespace Pokemon.Application.DTO
{
    public class PokemonEvolutionChainLinkResponseDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
        public int Id => int.Parse(Url.Split('/')[6]);
    }

}
