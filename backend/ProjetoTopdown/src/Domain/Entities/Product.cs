namespace ProjetoTopdown.Domain.Entities;

#pragma warning disable CS8618 //Considere adicionar o modificador "obrigatório".
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public decimal Price { get; private set; }
    public int StockQty { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private Product() { }


    public Product(string name, string sku, decimal price, int stockQty)
    {
        Name = name;
        Sku = sku;
        Price = price;
        StockQty = stockQty;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, decimal price, int stockQty, bool isActive)
    {
        Name = name;
        Price = price;
        StockQty = stockQty;
        IsActive = isActive;
    }

    public void DecreaseStock(int quantity)
    {
        if (StockQty < quantity) throw new InvalidOperationException("Estoque insuficiente.");
        StockQty -= quantity;
    }
}