using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.Application.Models.SchemaFilters;

#pragma warning disable CA1062
public class CreateOrderCommandSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CreateOrderCommand))
        {
            schema.Description = 
                "IMPORTANTE: Esta requisição requer um header 'Idempotency-Key'" +
                "com um GUID único para garantir a idempotência da operação.";
            
            schema.Example = new OpenApiObject
            {
                ["CustomerId"] = new OpenApiInteger(1),
                ["Items"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["ProductId"] = new OpenApiInteger(2),
                        ["Quantity"] = new OpenApiInteger(5)
                    },
                    new OpenApiObject
                    {
                        ["ProductId"] = new OpenApiInteger(5),
                        ["Quantity"] = new OpenApiInteger(1)
                    },
                    new OpenApiObject
                    {
                        ["ProductId"] = new OpenApiInteger(12),
                        ["Quantity"] = new OpenApiInteger(10)
                    }
                }
            };
        }
    }
}