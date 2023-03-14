namespace Pokemon.Application.DTO
{
    /// <summary>
    /// Representa um link de evolução de um Pokémon em resposta a uma solicitação feita à API pokeapi.co.
    /// </summary>
    public class PokemonEvolutionChainLinkResponseDTO
    {
        /// <summary>
        /// Obtém ou define o nome do Pokémon deste link de evolução.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define a URL do recurso do Pokémon deste link de evolução.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Obtém a URL da imagem do sprite do Pokémon deste link de evolução.
        /// </summary>
        public string SpriteUrl => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";

        /// <summary>
        /// Obtém o ID do Pokémon deste link de evolução a partir da sua URL.
        /// </summary>
        public int Id => int.Parse(Url.Split('/')[6]);
    }
}
