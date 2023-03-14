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

        /// <summary>
        /// Obtém uma lista de Pokémons com base em um limite opcional.
        /// </summary>
        /// <param name="limit">O limite máximo de Pokémons a serem retornados.</param>
        /// <returns>Uma lista de Pokémons.</returns>
        [HttpGet]
        public async Task<ActionResult<List<PokemonListingItemResponse>>> GetAllPokemonsAsync([FromQuery] int? limit = null)
        {
            var pokemons = await _pokemonService.GetAllPokemonsAsync(limit);
            return Ok(pokemons);
        }

        /// <summary>
        /// Busca um Pokémon pelo seu nome na API
        /// </summary>
        /// <param name="pokemonName">O nome do Pokémon a ser procurado</param>
        /// <returns>Um ActionResult contendo um PokemonDTO se o Pokémon for encontrado, caso contrário retorna NotFound</returns>
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

        /// <summary>
        /// Obtém informações sobre as evoluções de um Pokémon especificado.
        /// </summary>
        /// <param name="pokemonName">O nome do Pokémon a ser pesquisado.</param>
        /// <returns>Uma lista de informações sobre as evoluções do Pokémon.</returns>
        [HttpGet("{pokemonName}/evolutions")]
        public async Task<ActionResult<List<EvolutionDTO>>> GetPokemonEvolutionsAsync(string pokemonName)
        {
            var evolutions = await _pokemonService.GetPokemonEvolutionsAsync(pokemonName);
            return Ok(evolutions);
        }

        /// <summary>
        /// Obtém uma lista paginada de Pokémons.
        /// </summary>
        /// <param name="count">O número de Pokémons a serem retornados em cada página.</param>
        /// <returns>Uma lista paginada de Pokémons.</returns>
        [HttpGet("list")]
        public async Task<ActionResult<PokemonListingResponse>> GetPokemonsAsync([FromQuery] int count = 10)
        {
            var pokemonList = await _pokemonService.GetPokemonsAsync(count);
            return Ok(pokemonList);
        }

        /// <summary>
        /// Obtém informações sobre a espécie de um Pokémon especificado.
        /// </summary>
        /// <param name="pokemonName">O nome do Pokémon a ser pesquisado.</param>
        /// <returns>Informações sobre a espécie do Pokémon.</returns>
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

        /// <summary>
        /// Obtém uma lista de Pokémons aleatórios.
        /// </summary>
        /// <param name="count">O número de Pokémons a serem retornados.</param>
        /// <returns>Uma lista de Pokémons aleatórios.</returns>
        [HttpGet("random")]
        public async Task<ActionResult<List<PokemonDTO>>> GetRandomPokemonsAsync([FromQuery] int count = 10)
        {
            var randomPokemons = await _pokemonService.GetRandomPokemonsAsync(count);
            return Ok(randomPokemons);
        }

        /// <summary>
        /// Obtém o número total de Pokémons disponíveis na API
        /// </summary>
        /// <returns>O número total de Pokémons disponíveis como um ActionResult do tipo int</returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> TotalPokemonsCountAsync()
        {
            var count = await _pokemonService.GetTotalPokemonsCountAsync();
            return Ok(count);
        }

        /// <summary>
        /// Obtém todos os mestres Pokémon registrados no sistema.
        /// </summary>
        /// <returns>O número total de mestres Pokémon registrados no sistema.</returns>
        [HttpGet("masters")]
        public async Task<ActionResult<int>> GetAllPokemonMasters()
        {
            var masters = await _pokemonService.GetAllPokemonMasters();
            return Ok(masters);
        }

        /// <summary>
        /// Registra um novo mestre Pokémon
        /// </summary>
        /// <param name="master">As informações do mestre Pokémon a ser registrado</param>
        /// <returns>Um ActionResult contendo o ID do novo mestre Pokémon criado</returns>
        [HttpPost("masters/register")]
        public async Task<ActionResult<int>> RegisterPokemonMaster([Required] PokemonMasterDTO master)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newMaster = await _pokemonService.RegisterMasterPokemon(master);
            return Ok(newMaster);
        }

        /// <summary>
        /// Captura um Pokémon para um mestre Pokémon especificado.
        /// </summary>
        /// <param name="masterId">O ID do mestre Pokémon.</param>
        /// <param name="pokemonName">O nome do Pokémon a ser capturado.</param>
        /// <param name="forceCapture">Define se a captura deve ser forçada, mesmo que o Pokémon já tenha sido capturado anteriormente pelo mestre.</param>
        /// <returns>O número de Pokémons capturados pelo mestre até o momento.</returns>
        [HttpPost("{pokemonName}/{masterId}/capture")]
        public async Task<ActionResult<int>> CapturePokemon([Required] int masterId, [Required] string pokemonName, bool? forceCapture = true)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var count = await _pokemonService.CapturePokemonAsync(pokemonName, masterId, forceCapture.Value);
            return Ok(count);
        }

        /// <summary>
        /// Obtém todos os Pokémons capturados por um mestre Pokémon especificado.
        /// </summary>
        /// <param name="masterId">O ID do mestre Pokémon.</param>
        /// <returns>O número de Pokémons capturados pelo mestre até o momento.</returns>
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