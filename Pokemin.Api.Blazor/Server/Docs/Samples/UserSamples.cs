using Pokemon.Api.Utils;
using Pokemon.Application.DTO;
using Pokemon.Services;
using Swashbuckle.AspNetCore.Filters;
using Tynamix.ObjectFiller;

namespace Pokemon.Api.Docs.Samples
{
    public static class UserSamplesUtils
    {

    }

    public class CommonSamples
    {
        public class EmptyOkResult : IExamplesProvider<GetResponseDTO<object>>
        {
            public GetResponseDTO<object> GetExamples()
            {
                var result = new GetResponseDTO<object>();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }
    }

    public class PokemonOKSamples
    {
        public class GetPokemonsListing : IExamplesProvider<GetResponseDTO<List<PokemonListingItemResponse>>>
        {
            public GetResponseDTO<List<PokemonListingItemResponse>> GetExamples()
            {
                var result = new GetResponseDTO<List<PokemonListingItemResponse>>();
                result.Data = new Filler<PokemonListingItemResponse>().Create(5).ToList();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class SearchPokemon : IExamplesProvider<GetResponseDTO<PokemonDTO>>
        {
            public GetResponseDTO<PokemonDTO> GetExamples()
            {
                var result = new GetResponseDTO<PokemonDTO>();
                var filler = new Filler<PokemonDTO>();
                filler.Setup().OnProperty(x => x.SpriteUrl).IgnoreIt();
                filler.Setup().OnProperty(x => x.Types).IgnoreIt();
                result.Data = filler.Create();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class GetPokemons : IExamplesProvider<GetResponseDTO<PokemonListingResponse>>
        {
            public GetResponseDTO<PokemonListingResponse> GetExamples()
            {
                var result = new GetResponseDTO<PokemonListingResponse>();
                result.Data = new PokemonListingResponse { Results = new Filler<PokemonListingItemResponse>().Create(5).ToList() };
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class GetPokemonEvolutions : IExamplesProvider<GetResponseDTO<List<EvolutionDTO>>>
        {
            public GetResponseDTO<List<EvolutionDTO>> GetExamples()
            {
                var result = new GetResponseDTO<List<EvolutionDTO>>();
                result.Data = new Filler<EvolutionDTO>().Create(5).ToList();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class GetPokemonSpieceAsync : IExamplesProvider<GetResponseDTO<EvolutionSpeciesResponseDTO>>
        {
            public GetResponseDTO<EvolutionSpeciesResponseDTO> GetExamples()
            {
                var result = new GetResponseDTO<EvolutionSpeciesResponseDTO>();
                result.Data = new Filler<EvolutionSpeciesResponseDTO>().Create();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class GetRandomPokemonsAsync : IExamplesProvider<GetResponseDTO<List<PokemonDTO>>>
        {
            public GetResponseDTO<List<PokemonDTO>> GetExamples()
            {
                var result = new GetResponseDTO<List<PokemonDTO>>();
                var filler = new Filler<PokemonDTO>();
                filler.Setup().OnProperty(x => x.SpriteUrl).IgnoreIt();
                filler.Setup().OnProperty(x => x.Types).IgnoreIt();
                result.Data = filler.Create(5).ToList();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class TotalPokemonsCountAsync : IExamplesProvider<GetResponseDTO>
        {
            public GetResponseDTO GetExamples()
            {
                var result = new GetResponseDTO();
                result.Data = RandomGenerator.CreateNumber();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class GetAllPokemonMasters : IExamplesProvider<GetResponseDTO<List<PokemonMasterDTO>>>
        {
            public GetResponseDTO<List<PokemonMasterDTO>> GetExamples()
            {
                var result = new GetResponseDTO<List<PokemonMasterDTO>>();
                result.Data = new Filler<PokemonMasterDTO>().Create(5).ToList();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class CapturePokemon : IExamplesProvider<GetResponseDTO<CapturedPokemonDTO>>
        {
            public GetResponseDTO<CapturedPokemonDTO> GetExamples()
            {
                var result = new GetResponseDTO<CapturedPokemonDTO>();
                result.Data = new Filler<CapturedPokemonDTO>().Create();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class CaptureListPokemon : IExamplesProvider<GetResponseDTO<List<CapturedPokemonDTO>>>
        {
            public GetResponseDTO<List<CapturedPokemonDTO>> GetExamples()
            {
                var result = new GetResponseDTO<List<CapturedPokemonDTO>>();
                result.Data = new Filler<CapturedPokemonDTO>().Create(5).ToList();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }

        public class RegisterPokemonMaster : IExamplesProvider<GetResponseDTO<PokemonMasterDTO>>
        {
            public GetResponseDTO<PokemonMasterDTO> GetExamples()
            {
                var result = new GetResponseDTO<PokemonMasterDTO>();
                result.Data = new Filler<PokemonMasterDTO>().Create();
                result.StatusCode = System.Net.HttpStatusCode.OK;
                return result;
            }
        }
    }

    public class UserErrorsSamples
    {
        public class NotFound : IExamplesProvider<GetResponseDTO<object>>
        {
            public GetResponseDTO<object> GetExamples()
            {
                var result = new GetResponseDTO<object>();
                result.Errors = new string[1] { "Usuário não encontrado" };
                result.StatusCode = System.Net.HttpStatusCode.NotFound;
                return result;
            }
        }
        public class DataAlreadExists : IExamplesProvider<GetResponseDTO<object>>
        {
            public GetResponseDTO<object> GetExamples()
            {
                var result = new GetResponseDTO<object>();
                result.Errors = new string[] { "Usuário já cadastrado com este Email.", "Usuário já cadastrado com este CPF.", "Usuário já cadastrado com este CNS." };
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return result;
            }
        }
        public class RandomBadRequest : IExamplesProvider<GetResponseDTO<object>>
        {
            public GetResponseDTO<object> GetExamples()
            {
                var result = new GetResponseDTO<object>();
                result.Errors = new string[] { RandomGenerator.CreateString(12), RandomGenerator.CreateString(10), RandomGenerator.CreateString(8) };
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return result;
            }
        }
        public class InternalServerError : IExamplesProvider<GetResponseDTO<object>>
        {
            public GetResponseDTO<object> GetExamples()
            {
                var result = new GetResponseDTO<object>();
                result.Errors = new string[1] { RandomGenerator.CreateString(20) };
                result.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return result;
            }
        }


        PokemonDTO[] pokemonList => new PokemonDTO[]
        {
            new PokemonDTO { Id = 1, Name = "Bulbasaur" },
            new PokemonDTO { Id = 2, Name = "Ivysaur" },
            new PokemonDTO { Id = 3, Name = "Venusaur" },
            new PokemonDTO { Id = 4, Name = "Charmander" },
            new PokemonDTO { Id = 5, Name = "Charmeleon" },
            new PokemonDTO { Id = 6, Name = "Charizard" },
            new PokemonDTO { Id = 7, Name = "Squirtle" },
            new PokemonDTO { Id = 8, Name = "Wartortle" },
            new PokemonDTO { Id = 9, Name = "Blastoise" },
            new PokemonDTO { Id = 10, Name = "Caterpie" },
            new PokemonDTO { Id = 11, Name = "Metapod" },
            new PokemonDTO { Id = 12, Name = "Butterfree" },
            new PokemonDTO { Id = 13, Name = "Weedle" },
            new PokemonDTO { Id = 14, Name = "Kakuna" },
            new PokemonDTO { Id = 15, Name = "Beedrill" },
            new PokemonDTO { Id = 16, Name = "Pidgey" },
            new PokemonDTO { Id = 17, Name = "Pidgeotto" },
            new PokemonDTO { Id = 18, Name = "Pidgeot" },
            new PokemonDTO { Id = 19, Name = "Rattata" },
            new PokemonDTO { Id = 20, Name = "Raticate" },
            new PokemonDTO { Id = 21, Name = "Spearow" },
            new PokemonDTO { Id = 22, Name = "Fearow" },
            new PokemonDTO { Id = 23, Name = "Ekans" },
            new PokemonDTO { Id = 24, Name = "Arbok" },
            new PokemonDTO { Id = 25, Name = "Pikachu" },
            new PokemonDTO { Id = 26, Name = "Raichu" },
            new PokemonDTO { Id = 27, Name = "Sandshrew" },
            new PokemonDTO { Id = 28, Name = "Sandslash" },
            new PokemonDTO { Id = 29, Name = "Nidoran♀" },
            new PokemonDTO { Id = 30, Name = "Nidorina" },
            new PokemonDTO { Id = 31, Name = "Nidoqueen" },
            new PokemonDTO { Id = 32, Name = "Nidoran♂" },
            new PokemonDTO { Id = 33, Name = "Nidorino" },
            new PokemonDTO { Id = 34, Name = "Nidoking" },
            new PokemonDTO { Id = 35, Name = "Clefairy" },
            new PokemonDTO { Id = 36, Name = "Clefable" },
            new PokemonDTO { Id = 37, Name = "Vulpix" },
            new PokemonDTO { Id = 38, Name = "Ninetales" },
            new PokemonDTO { Id = 39, Name = "Jigglypuff" },
            new PokemonDTO { Id = 40, Name = "Wigglytuff" },
            new PokemonDTO { Id = 41, Name = "Zubat" },
            new PokemonDTO { Id = 42, Name = "Golbat" }
        };
    }
}
