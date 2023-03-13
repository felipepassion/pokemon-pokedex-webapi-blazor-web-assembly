namespace Pokemon.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Pokemon.Application.DTO;

    public class PokemonService
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        public PokemonService()
        {
            _client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<EvolutionDTO>> GetPokemonEvolutions(string pokemonName)
        {
            var httpClient = new HttpClient();

            // Obtemos as informações de espécie do Pokémon
            var speciesResponse = await httpClient.GetFromJsonAsync<EvolutionSpeciesResponseDTO>($"https://pokeapi.co/api/v2/pokemon-species/{pokemonName}/");

            // Obtemos as informações de evolução da cadeia de evolução do Pokémon
            var evolutionChainResponse = await httpClient.GetFromJsonAsync<EvolutionChainDTO>(speciesResponse.EvolutionChain.Url);

            // Mapeamos a cadeia de evolução para uma lista de evoluções
            var evolutions = new List<EvolutionDTO>();
            var evolutionChain = evolutionChainResponse.Chain;
            while (evolutionChain != null)
            {
                if (evolutionChain.Species != null)
                {
                    evolutions.Add(new EvolutionDTO
                    {
                        Name = evolutionChain.Species.Name,
                        Level = evolutionChain.EvolutionDetails?.FirstOrDefault()?.MinLevel
                    });
                }
                evolutionChain = evolutionChain.EvolvesTo?.FirstOrDefault();
            }

            return evolutions;
        }

        public async Task<List<PokemonListingItemResponse>> GetPokemons(int count = 10)
        {
            var response = await _client.GetAsync($"pokemon?limit={count}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PokemonListingResponse>(content);

            return apiResponse.Results;
        }

        private void ExtractEvolutions(EvolutionChainLinkDTO chain, List<EvolutionDTO> evolutions)
        {
            foreach (var evolution in chain.EvolvesTo)
            {
                evolutions.Add(new EvolutionDTO
                {
                    Name = evolution.Species.Name,
                    Level = evolution.EvolutionDetails?[0]?.MinLevel
                });
                ExtractEvolutions(evolution, evolutions);
            }
        }

        public async Task<List<PokemonListingItemResponse>> GetRandomPokemons(int count = 10)
        {
            var totalPokemonsCount = await TotalPkemonsCount();
            var allPokemon = await GetAllPokemons(totalPokemonsCount);
            var random = new Random();
            var result = new List<PokemonListingItemResponse>();

            while (result.Count < count)
            {
                var index = random.Next(0, totalPokemonsCount);
                var pokemon = allPokemon[index];

                // Verificar se o Pokémon já foi adicionado à lista
                if (!result.Any(p => p.Name == pokemon.Name))
                {
                    result.Add(pokemon);
                }
            }

            return result;
        }

        /// <summary>
        /// Se o limite for nulo, ele busca todos os pokemons dentro do limite total
        /// </summary>
        /// <param name="limit">Limite total</param>
        /// <returns>Lista de pokemons resumida contendo Nome + Url</returns>
        public async Task<List<PokemonListingItemResponse>> GetAllPokemons(int? limit = null)
        {
            var allPokemon = new List<PokemonListingItemResponse>();
            var url = "pokemon?limit=1000";

            while (url != null)
            {
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<PokemonListingResponse>(content);
                allPokemon.AddRange(apiResponse.Results);
                url = apiResponse.Next;
            }

            return allPokemon;
        }

        public async Task<int> TotalPkemonsCount() => (await GetPokemons(1)).Count;
    }
}