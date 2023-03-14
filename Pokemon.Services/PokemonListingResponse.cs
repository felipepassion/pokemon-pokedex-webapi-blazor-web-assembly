namespace Pokemon.Services
{
    using System.Collections.Generic;

    public class PokemonListingResponse
    {
        public int Count => this.Results?.Count ?? 0;
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<PokemonListingItemResponse> Results { get; set; }
    }

    public class PokemonListingItemResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}