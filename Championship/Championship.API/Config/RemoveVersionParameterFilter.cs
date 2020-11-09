using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Championship.API.Config
{
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Any(p => p.Name == "version"))
            {
                var versionParameter = operation.Parameters.Single(p => p.Name == "version");
                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}
