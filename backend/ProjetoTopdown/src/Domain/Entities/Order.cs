using ProjetoTopdown.Domain.Enums;

namespace ProjetoTopdown.Domain.Entities;

#pragma warning disable CS8618 //Considere adicionar o modificador "obrigatório".
public class Order
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    //SUGESTÃO: Aqui eu acho que o proprio backend possa gerar o IdempotencyKey com base em:
    //itens, cliente e data/hora.
    public Guid IdempotencyKey { get; private set; }

    public Customer Customer { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    private Order() { }

    public Order(int customerId, Guid idempotencyKey)
    {
        CustomerId = customerId;
        Status = OrderStatus.CREATED;
        CreatedAt = DateTime.UtcNow;
        IdempotencyKey = idempotencyKey;
    }

    public void CalculateTotalAmount()
    {
        TotalAmount = OrderItems.Sum(item => item.LineTotal);
    }
}