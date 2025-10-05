using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.WebApi.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.WebApi.Filters;

public class IdempotencyKeyHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ArgumentNullException.ThrowIfNull(operation);
        ArgumentNullException.ThrowIfNull(context);

        var hasIdempotencyKeyAttribute = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .Any(em => em is IdempotencyKeyRequiredAttribute);

        if (!hasIdempotencyKeyAttribute)
        {
            return;
        }

        if (operation.Parameters.Any(p => p.Name == "Idempotency-Key"))
        {
            return;
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Idempotency-Key",
            In = ParameterLocation.Header,
            Description = "Chave de idempotência para previnir a criação duplicada de recursos.",
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Format = "uuid",
                Example = new OpenApiString(Guid.NewGuid().ToString())
            }
        });
    }
}