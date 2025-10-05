using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.Application.Models.SchemaFilters;

#pragma warning disable CA1062 // Validar argumentos de métodos públicos
public class CreateProductCommandSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {        
        if (context.Type == typeof(CreateProductCommand))
        {
            schema.Example = new OpenApiObject
            {     
                ["Name"] = new OpenApiString("Teclado Mecânico RGB"),
                ["Sku"] = new OpenApiString("SKU-TEC-RGB-01"),
                ["Price"] = new OpenApiDouble(349.90),
                ["StockQty"] = new OpenApiInteger(50)
            };
        }
    }
}