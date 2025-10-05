using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.Application.Models.SchemaFilters;

#pragma warning disable CA1062 // Validar argumentos de métodos públicos
public class CreateCustomerCommandSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CreateCustomerCommand))
        {
            schema.Example = new OpenApiObject
            {
                ["Name"] = new OpenApiString("Gabriel Galter"),
                ["Email"] = new OpenApiString("gabriel.galter@example.com"),
                ["Document"] = new OpenApiString("123.456.789-00")
            };
        }
    }
}