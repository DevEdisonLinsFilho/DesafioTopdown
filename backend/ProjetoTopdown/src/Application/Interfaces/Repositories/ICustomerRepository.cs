using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Application.Interfaces.Repositories;

public interface ICustomerRepository
{
    #region Dapper
    Task<PagedResult<Customer>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm,
        CancellationToken cancellationToken = default);
    #endregion

    #region EF Core
    Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Customer customer,
        CancellationToken cancellationToken = default);

    Task<bool> HasOrdersAsync(int customerId, CancellationToken cancellationToken = default);
    #endregion
}