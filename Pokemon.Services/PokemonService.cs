namespace Pokemon.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Pokemon.Application.DTO;
    using Pokemon.Data;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public interface IPokemonService
    {
        Task<PokemonDTO> CapturePokemonAsync(string name, int masterId);
        Task<List<PokemonListingItemResponse>> GetAllPokemonsAsync(int? limit = null);
        Task<PokemonDTO> SearchPokemonAsync(string pokemonName);
        Task<List<EvolutionDTO>> GetPokemonEvolutionsAsync(string pokemonName);
        Task<PokemonListingResponse> GetPokemonsAsync(int count = 10);
        Task<EvolutionSpeciesResponseDTO?> GetPokemonSpieceAsync(string pokemonName);
        Task<List<PokemonDTO>> GetRandomPokemonsAsync(int count = 10);
        Task<int> TotalPokemonsCountAsync();
    }

    public class PokemonService : IPokemonService
    {
        private readonly IPokemonDatabase _pokemonDatabase;
        HttpClient _client;
        public PokemonService(IPokemonDatabase pokemonDatabase, HttpClient client)
        {
            this._pokemonDatabase = pokemonDatabase;
            this._client = client;
        }

        public async Task<List<EvolutionDTO>> GetPokemonEvolutionsAsync(string pokemonName)
        {
            // Obtemos as informações de espécie do Pokémon
            EvolutionSpeciesResponseDTO? speciesResponse = await GetPokemonSpieceAsync(pokemonName);

            // Obtemos as informações de evolução da cadeia de evolução do Pokémon
            var evolutionChainResponse = await _client.GetFromJsonAsync<EvolutionChainDTO>(speciesResponse.Evolution_Chain.Url);
            if (evolutionChainResponse is null) throw new Exception($"Cadeia de evolução do {pokemonName} não encontrada");

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
                        Level = evolutionChain.Evolution_Details?.FirstOrDefault()?.MinLevel
                    });
                }
                evolutionChain = evolutionChain.Evolves_To?.FirstOrDefault();
            }

            return evolutions;
        }

        public async Task<EvolutionSpeciesResponseDTO?> GetPokemonSpieceAsync(string pokemonName)
        {
            try
            {
                var pokemon = await SearchPokemonAsync(pokemonName);
                var speciesResponse = await _client.GetFromJsonAsync<EvolutionSpeciesResponseDTO>(pokemon.Species.Url);
                if (speciesResponse is null) throw new Exception($"Espécie do Pokemon {pokemonName} não encontrada");
                return speciesResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PokemonDTO> SearchPokemonAsync(string pokemonName)
        {
            var httpClient = new HttpClient();

            // Obtemos as informações básicas do Pokémon
            var pokemonResponse = await httpClient.GetFromJsonAsync<PokemonDTO>($"https://pokeapi.co/api/v2/pokemon/{pokemonName}/");

            if (pokemonResponse is null) throw new Exception($"Pokemon {pokemonName} não encontrado");

            var speciesResponse = await _client.GetFromJsonAsync<EvolutionSpeciesResponseDTO>(pokemonResponse.Species.Url);
            pokemonResponse.Capture_Rate = speciesResponse.Capture_Rate;
            return pokemonResponse;
        }

        public async Task<PokemonListingResponse> GetPokemonsAsync(int count = 10)
        {
            var response = await _client.GetAsync($"pokemon?limit={count}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PokemonListingResponse>(content);

            return apiResponse;
        }

        private void ExtractEvolutions(EvolutionChainLinkDTO chain, List<EvolutionDTO> evolutions)
        {
            foreach (var evolution in chain.Evolves_To)
            {
                evolutions.Add(new EvolutionDTO
                {
                    Name = evolution.Species.Name,
                    Level = evolution.Evolution_Details?[0]?.MinLevel
                });
                ExtractEvolutions(evolution, evolutions);
            }
        }

        public async Task<List<PokemonDTO>> GetRandomPokemonsAsync(int count = 10)
        {
            var totalPokemonsCount = await TotalPokemonsCountAsync();
            var allPokemon = await GetAllPokemonsAsync(totalPokemonsCount);
            var random = new Random();
            var result = new List<PokemonDTO>();

            while (result.Count < count)
            {
                var index = random.Next(0, totalPokemonsCount);
                var pokemon = allPokemon[index];

                // Verificar se o Pokémon já foi adicionado à lista
                if (!result.Any(p => p.Name == pokemon.Name))
                {
                    result.Add(await SearchPokemonAsync(pokemon.Name));
                }
            }

            return result;
        }

        /// <summary>
        /// Se o limite for nulo, ele busca todos os pokemons dentro do limite total
        /// </summary>
        /// <param name="limit">Limite total</param>
        /// <returns>Lista de pokemons resumida contendo Nome + Url</returns>
        public async Task<List<PokemonListingItemResponse>> GetAllPokemonsAsync(int? limit = null)
        {
            var allPokemon = new List<PokemonListingItemResponse>();
            var url = $"pokemon?limit={limit ?? 1000}";

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

        public async Task<int> TotalPokemonsCountAsync() => (await GetPokemonsAsync(1)).Count;

        public async Task<PokemonDTO> CapturePokemonAsync(string name, int masterId)
        {
            var pokemon = await SearchPokemonAsync(name);

            // Verifica se o Pokémon foi capturado com base na taxa de captura aleatória
            if (TryCapturePokemon(pokemon, out float rate))
            {
                // Adiciona o Pokémon à tabela de capturados no banco de dados

                await _pokemonDatabase.SaveCapturedPokemonAsync(pokemon, masterId);

                return pokemon;
            }
            else
            {
                throw new Exception($"Pokemon não capturado com base no rate {rate}/{pokemon.Capture_Rate}");
            }
        }

        public bool TryCapturePokemon(PokemonDTO pokemon, out float rate)
        {
            var random = new Random();
            rate = random.Next(256);
            return rate <= pokemon.Capture_Rate;
        }
    }
}