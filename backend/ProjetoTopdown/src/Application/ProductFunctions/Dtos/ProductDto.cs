namespace ProjetoTopdown.Application.ProductFunctions.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; }
}