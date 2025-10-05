namespace ProjetoTopdown.Domain.Entities;

#pragma warning disable CS8618 //Considere adicionar o modificador "obrigatório".
public class OrderItem
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal LineTotal { get; private set; }
    public Order Order { get; private set; }
    public Product Product { get; private set; }

    private OrderItem() { }

    public OrderItem(int orderId, int productId, decimal unitPrice, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        LineTotal = unitPrice * quantity;
    }
}