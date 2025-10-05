using Microsoft.EntityFrameworkCore;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Infrastructure.Persistence.Repositories.OrderPersistence;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Order?> GetByIdempotencyKeyAsync(
    Guid idempotencyKey,
    CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.IdempotencyKey == idempotencyKey, cancellationToken)
            .ConfigureAwait(false);
    }
}