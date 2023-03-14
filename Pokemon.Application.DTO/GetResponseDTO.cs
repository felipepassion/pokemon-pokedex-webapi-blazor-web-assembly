using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Pokemon.Application.DTO
{
    public class GetResponseDTO : GetResponseDTO<object>
    {

    }
    public class GetResponseDTO<T>
        where T : class
    {
        public GetResponseDTO(T response)
            : this(HttpStatusCode.OK, response)
        {
        }

        public GetResponseDTO(HttpStatusCode code, T response)
        {
            this.StatusCode = code;
            this.Data = response;
        }

        public GetResponseDTO(params string[] errors)
            : this(HttpStatusCode.InternalServerError, errors: errors)
        {
        }

        public GetResponseDTO(HttpStatusCode code, params string[] errors)
        {
            this.StatusCode = code;
            this.Errors = errors?.Where(x => x != null).ToArray();
        }

        public bool Success
        {
            get { return this.Errors?.Any() != true && this.StatusCode >= HttpStatusCode.OK && this.StatusCode < HttpStatusCode.Ambiguous; }
        }

        public static GetResponseDTO<T> Ok(T response = null)
        {
            return new GetResponseDTO<T>(response);
        }

        public static GetResponseDTO<T> Error(params string[] errors)
        {
            return Error(HttpStatusCode.InternalServerError, errors);
        }

        public static GetResponseDTO<T> BadRequest(params string[] errors)
        {
            return Error(HttpStatusCode.BadRequest, errors);
        }

        public static GetResponseDTO<T> NotFound(params string[] errors)
        {
            return Error(HttpStatusCode.NotFound, errors);
        }

        public static GetResponseDTO<T> BadRequest(ModelStateDictionary state)
        {
            return ConstructErrorMessages(state);
        }

        public static GetResponseDTO<T> Error(HttpStatusCode statusCode, params string[] errors)
        {
            return new GetResponseDTO<T>(statusCode, errors);
        }

        public static GetResponseDTO<T> Error(HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            return new GetResponseDTO<T>(statusCode, errors: errors.ToArray());
        }

        public void AddError(params string[] newErrors)
        {
            var list = this.Errors?.ToList() ?? new List<string>();
            list.AddRange(newErrors);
            this.Errors = list.ToArray();
        }

        public HttpStatusCode StatusCode { get; set; }
        public string[] Errors { get; set; }
        public T Data { get; set; }

        public static GetResponseDTO<T> ConstructErrorMessages(ModelStateDictionary ModelState)
        {
            var result = new GetResponseDTO<T>(HttpStatusCode.BadRequest);

            foreach (var keyModelStatePair in ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        result.AddError(errorMessage);
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }

                        result.AddError(errorMessages.ToArray());
                    }
                }
            }
            return result;
        }

        static string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
            "O campo não é válido." :
            error.ErrorMessage;
        }
    }

}