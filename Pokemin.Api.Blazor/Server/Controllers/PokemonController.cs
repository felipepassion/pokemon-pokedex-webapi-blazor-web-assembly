using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.DTO;
using Pokemon.Services;
using System.ComponentModel.DataAnnotations;

namespace Pokemin.Api.Blazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PokemonListingItemResponse>>> GetAllPokemonsAsync([FromQuery] int? limit = null)
        {
            var pokemons = await _pokemonService.GetAllPokemonsAsync(limit);
            return Ok(pokemons);
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<PokemonDTO>> SearchPokemonAsync(string pokemonName)
        {
            var pokemon = await _pokemonService.SearchPokemonAsync(pokemonName);
            if (pokemon == null)
            {
                return NotFound();
            }
            return Ok(pokemon);
        }

        [HttpGet("{pokemonName}/evolutions")]
        public async Task<ActionResult<List<EvolutionDTO>>> GetPokemonEvolutionsAsync(string pokemonName)
        {
            var evolutions = await _pokemonService.GetPokemonEvolutionsAsync(pokemonName);
            return Ok(evolutions);
        }

        [HttpGet("list")]
        public async Task<ActionResult<PokemonListingResponse>> GetPokemonsAsync([FromQuery] int count = 10)
        {
            var pokemonList = await _pokemonService.GetPokemonsAsync(count);
            return Ok(pokemonList);
        }

        [HttpGet("{pokemonName}/species")]
        public async Task<ActionResult<EvolutionSpeciesResponseDTO>> GetPokemonSpieceAsync(string pokemonName)
        {
            var species = await _pokemonService.GetPokemonSpieceAsync(pokemonName);
            if (species == null)
            {
                return NotFound();
            }
            return Ok(species);
        }

        [HttpGet("random")]
        public async Task<ActionResult<List<PokemonDTO>>> GetRandomPokemonsAsync([FromQuery] int count = 10)
        {
            var randomPokemons = await _pokemonService.GetRandomPokemonsAsync(count);
            return Ok(randomPokemons);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> TotalPokemonsCountAsync()
        {
            var count = await _pokemonService.GetTotalPokemonsCountAsync();
            return Ok(count);
        }

        [HttpGet("masters")]
        public async Task<ActionResult<int>> GetAllPokemonMasters()
        {
            var masters = await _pokemonService.GetAllPokemonMasters();
            return Ok(masters);
        }

        [HttpPost("masters/register")]
        public async Task<ActionResult<int>> RegisterPokemonMaster([Required] PokemonMasterDTO master)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newMaster = await _pokemonService.RegisterMasterPokemon(master);
            return Ok(newMaster);
        }

        [HttpPost("{pokemonName}/{masterId}/capture")]
        public async Task<ActionResult<int>> CapturePokemon([Required] int masterId, [Required] string pokemonName, bool? forceCapture = true)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var count = await _pokemonService.CapturePokemonAsync(pokemonName, masterId, forceCapture.Value);
            return Ok(count);
        }

        [HttpGet("captured/{masterId}")]
        [HttpGet("captured")]
        public async Task<ActionResult<int>> GetAllCapturedPokemons(int? masterId = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allCapturedPokemons = await _pokemonService.GetAllCapturedPokemons(masterId);
            return Ok(allCapturedPokemons);
        }
    }
}