using FluentAssertions;
using Pokemon.Services;

namespace Pokemon.Tests
{
    public class PokemonServiceTests
    {
        PokemonService _service;
        public PokemonServiceTests()
        {
            _service = new PokemonService();
        }

        [Fact]
        public async Task GetTotalPokemons()
        {
            var total = await _service.TotalPkemonsCount();
            (total > 0).Should().Be(true);
        }

        [Fact]
        public async Task GetAllPokemons()
        {
            var total = await _service.TotalPkemonsCount();
            var pokemons = await _service.GetAllPokemons();
            pokemons.Count.Should().Be(total);
            pokemons.All(x => x.Name != null && x.Url != null).Should().Be(true);
        }

        [Fact]
        public async Task GetRandomPokemons()
        {
            var limit = 10;
            var pokemons = await _service.GetRandomPokemons(limit);
            pokemons.Count.Should().Be(limit);
            pokemons.All(x => x.Name != null && x.Url != null).Should().Be(true);
            // Testar se os pokemons são únicos
            pokemons.Select(x => x.Name).Distinct().Count().Should().Be(limit);
        }

        [Fact]
        public async Task GetPokemonEvolutions()
        {
            var limit = 1;
            var randomPokemon = (await _service.GetRandomPokemons(limit)).FirstOrDefault();
            randomPokemon.Should().NotBe(null);
            randomPokemon.Name.Should().NotBe(null);

            var evolutions = await _service.GetPokemonEvolutions(randomPokemon.Name);
        }
    }
}