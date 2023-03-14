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

    public class UserOkSamples
    {
        public class UserFound : IExamplesProvider<GetResponseDTO<PokemonListingResponse>>
        {
            public GetResponseDTO<PokemonListingResponse> GetExamples()
            {
                var result = new GetResponseDTO<PokemonListingResponse>();
                result.Data = new PokemonListingResponse
                {
                    Results = new Filler<PokemonListingItemResponse>().Create(5).ToList()
                };
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
    }
}
