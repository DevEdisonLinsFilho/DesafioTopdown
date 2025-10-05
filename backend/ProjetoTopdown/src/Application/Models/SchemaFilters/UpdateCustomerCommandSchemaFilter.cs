using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.Application.Models.SchemaFilters;
#pragma warning disable CA1062 // Validar argumentos de métodos públicos

public class UpdateCustomerCommandSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(UpdateCustomerCommand))
        {
            schema.Example = new OpenApiObject
            {
                ["Name"] = new OpenApiString("Gabriel Galter (Atualizado)"),
                ["Email"] = new OpenApiString("gabriel.galter@example.com"),
                ["Document"] = new OpenApiString("123.456.789-00")
            };
        }
    }
}