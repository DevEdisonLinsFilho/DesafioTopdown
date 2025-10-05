using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdempotencyKeyAsync(
            Guid idempotencyKey,
            CancellationToken cancellationToken = default);

}