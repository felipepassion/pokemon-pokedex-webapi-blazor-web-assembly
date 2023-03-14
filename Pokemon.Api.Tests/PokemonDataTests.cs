using FluentAssertions;
using Pokemon.Application.DTO;
using Tynamix.ObjectFiller;

namespace Pokemon.Tests
{
    public class PokemonDataTests : BaseTests
    {
        [Fact]
        public void DatabaseInitialization()
        {
            // Se o construtor for chamado e não der erro, quer dizer que as tabelas foram criadas.
            this.PokemonDatabase.ClearDatabase();
        }

        [Fact]
        public async Task SaveMasterPokemon()
        {
            var randomMaster = new Filler<PokemonMasterDTO>().Create();
            await this.PokemonDatabase.SaveMasterPokemonAsync(randomMaster);
            var existing = await this.PokemonDatabase.SearchMasterPokemonByNameAsync(randomMaster.Name);
            existing.Id.Should().NotBe(null);
            existing.Name.Should().Be(randomMaster.Name);
        }

        [Fact]
        public async Task GetAllMasters()
        {
            var randomMaster = new Filler<PokemonMasterDTO>().Create();
            await this.PokemonDatabase.SaveMasterPokemonAsync(randomMaster);

            var masters = await this.PokemonDatabase.GetAllPokemonMastersAsync();

            masters.Should().NotBeNull();
            (masters.Count > 0).Should().Be(true);
        }

        [Fact]
        public async Task CapturePokemon()
        {
            var randomMaster = new Filler<PokemonMasterDTO>().Create();
            await this.PokemonDatabase.SaveMasterPokemonAsync(randomMaster);
            var masterInDb = await this.PokemonDatabase.SearchMasterPokemonByNameAsync(randomMaster.Name);

            var masterId = masterInDb.Id;
            var randomPokemon = (await this.PokemonService.GetRandomPokemonsAsync(1)).FirstOrDefault();
            await this.PokemonDatabase.SaveCapturedPokemonAsync(randomPokemon, masterInDb);

            var existingCaptured = (await this.PokemonDatabase.GetAllCapturedPokemonsAsync(masterId))
                .FirstOrDefault(x => x.PokemonName == randomPokemon.Name);
            existingCaptured.Should().NotBeNull();
            existingCaptured.PokemonName.Should().Be(randomPokemon.Name);
        }

        [Fact]
        public async Task GetAllCapturedPokemons()
        {
            var newMaster = await this.PokemonDatabase.SaveMasterPokemonAsync(new Filler<PokemonMasterDTO>().Create());
            var randomPokemon = (await this.PokemonService.GetRandomPokemonsAsync(1)).FirstOrDefault();
            var newCapturedPokemon = await this.PokemonDatabase.SaveCapturedPokemonAsync(randomPokemon, newMaster);
            var allCapturedPokemons = await this.PokemonDatabase.GetAllCapturedPokemonsAsync(newMaster.Id);

            var myCapturedPokemon = allCapturedPokemons.FirstOrDefault(x=>x.PokemonId == randomPokemon.Id);
            myCapturedPokemon.Should().NotBeNull();
        }
    }
}