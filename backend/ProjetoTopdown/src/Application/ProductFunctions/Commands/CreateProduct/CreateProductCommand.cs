using MediatR;
using ProjetoTopdown.Application.Models.SchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;

[SwaggerSchema("Model para cadastrar um novo produto")]
[SwaggerSchemaFilter(typeof(CreateProductCommandSchemaFilter))]
public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
}