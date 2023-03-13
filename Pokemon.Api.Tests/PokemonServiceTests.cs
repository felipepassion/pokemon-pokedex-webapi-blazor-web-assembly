using FluentAssertions;
using Pokemon.Services;

namespace Pokemon.Tests
{
    public class PokemonServiceTests : BaseTests
    {
        [Fact]
        public async Task GetTotalPokemons()
        {
            var total = await PokemonService.TotalPokemonsCountAsync();
            (total > 0).Should().Be(true);
        }

        [Fact]
        public async Task GetAllPokemons()
        {
            var total = await PokemonService.TotalPokemonsCountAsync();
            var pokemons = await PokemonService.GetAllPokemonsAsync();
            pokemons.Count.Should().Be(total);
            pokemons.All(x => x.Name != null && x.Url != null).Should().Be(true);
        }

        [Fact]
        public async Task GetRandomPokemons()
        {
            var limit = 10;
            var pokemons = await PokemonService.GetRandomPokemonsAsync(limit);
            pokemons.Count.Should().Be(limit);
            pokemons.All(x => x.Name != null).Should().Be(true);
            // Testar se os pokemons são únicos
            pokemons.Select(x => x.Name).Distinct().Count().Should().Be(limit);
        }

        [Fact]
        public async Task GetPokemonEvolutions()
        {
            var limit = 1;
            var randomPokemon = (await PokemonService.GetRandomPokemonsAsync(limit)).FirstOrDefault();
            randomPokemon.Should().NotBe(null);
            randomPokemon.Name.Should().NotBe(null);

            var evolutions = await PokemonService.GetPokemonEvolutionsAsync(randomPokemon.Id.ToString());
        }
    }
}