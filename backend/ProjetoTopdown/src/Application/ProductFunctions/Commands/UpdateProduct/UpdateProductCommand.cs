using MediatR;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{    
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; }
}