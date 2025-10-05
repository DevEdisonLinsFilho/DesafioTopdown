using MediatR;
using ProjetoTopdown.Application.OrderFunctions.Dtos;
using System.Collections.ObjectModel;

#pragma warning disable CA1002 // Não expor listas genéricas
#pragma warning disable CA2227 // As propriedades de coleção devem ser somente leitura
namespace ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public Guid IdempotencyKey { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();

    }
}
