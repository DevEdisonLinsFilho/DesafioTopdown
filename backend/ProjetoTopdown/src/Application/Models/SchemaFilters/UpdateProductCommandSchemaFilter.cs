using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoTopdown.Application.Models.SchemaFilters;

#pragma warning disable CA1062

public class UpdateProductCommandSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(UpdateProductCommand))
        {
            schema.Example = new OpenApiObject
            {
                ["Name"] = new OpenApiString("Teclado Mecânico RGB (Atualizado)"),
                ["Price"] = new OpenApiDouble(399.99),
                ["StockQty"] = new OpenApiInteger(45),
                ["IsActive"] = new OpenApiBoolean(true)
            };
        }
    }
}