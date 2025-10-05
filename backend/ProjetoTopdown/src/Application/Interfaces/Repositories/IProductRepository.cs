using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Domain.Entities;
namespace ProjetoTopdown.Application.Interfaces.Repositories;

public interface IProductRepository
{
    #region Dapper
    Task<PagedResult<Product>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm,
            CancellationToken cancellationToken);

    #endregion

    Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);


    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Product product,
            CancellationToken cancellationToken = default);

    Task<List<Product>> GetByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken cancellationToken = default);
}