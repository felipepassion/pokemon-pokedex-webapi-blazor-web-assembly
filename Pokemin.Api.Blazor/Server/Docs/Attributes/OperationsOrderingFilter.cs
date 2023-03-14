using Microsoft.OpenApi.Models;
using Pokemon.Api.Docs.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pokemon.Api.Docs.Attributes
{
    public class OperationsOrderingFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument openApiDoc, DocumentFilterContext context)
        {
            var notMapped = new List<KeyValuePair<string, OpenApiPathItem>>();

            Dictionary<KeyValuePair<string, OpenApiPathItem>, int> paths = new Dictionary<KeyValuePair<string, OpenApiPathItem>, int>();

            foreach (var path in openApiDoc.Paths)
            {
                var x = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                    .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase));
                var y = x.ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is OperationOrderAttribute) as OperationOrderAttribute;

                OperationOrderAttribute orderAttribute = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                    .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase))?
                    .ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is OperationOrderAttribute) as OperationOrderAttribute;

                if (orderAttribute == null)
                    notMapped.Add(path);
                //throw new ArgumentNullException("there is no order for operation " + path.Key);
                else
                {
                    int order = orderAttribute.Order;
                    paths.Add(path, order);
                }
            }

            foreach (var item in notMapped)
            {
                paths.Add(item, paths.Count);
            }

            var orderedPaths = paths.OrderBy(x => x.Value).ToList();

            openApiDoc.Paths.Clear();
            orderedPaths.ForEach(x => openApiDoc.Paths.Add(x.Key.Key, x.Key.Value));
        }

    }
}
