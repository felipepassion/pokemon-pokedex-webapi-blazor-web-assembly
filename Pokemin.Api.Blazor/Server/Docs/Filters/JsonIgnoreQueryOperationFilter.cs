using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Api.Docs.Filters
{
    public class JsonIgnoreQueryOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.ParameterDescriptions
                .Where(d => true).ToList()
                .ForEach(param =>
                {
                    var toIgnore =
                        ((DefaultModelMetadata)param.ModelMetadata)
                        .Attributes.PropertyAttributes
                        ?.Any(x => x is JsonIgnoreAttribute);

                    var toRemove = operation.Parameters
                        .SingleOrDefault(p => p.Name.Equals(param.Name, StringComparison.InvariantCultureIgnoreCase));

                    if (toIgnore ?? false)
                        operation.Parameters.Remove(toRemove);
                });
        }
    }
}
