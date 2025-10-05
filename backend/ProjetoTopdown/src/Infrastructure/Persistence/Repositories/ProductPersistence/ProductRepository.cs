using Dapper;
using Microsoft.EntityFrameworkCore;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Domain.Entities;
using System.Data;

namespace ProjetoTopdown.Infrastructure.Persistence.Repositories.ProductPersistence;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IDbConnection _dbConnection;

    public ProductRepository(ApplicationDbContext context, IDbConnection dbConnection)
    {
        _context = context;
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Implementação com Dapper para leitura paginada.
    /// </summary>
    public async Task<PagedResult<Product>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm,
        CancellationToken cancellationToken)
    {
        var whereClause = "";
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            whereClause = @"WHERE ""Name"" ILIKE @SearchTerm OR ""Sku"" ILIKE @SearchTerm";
        }

        var sql = $@"
            SELECT * FROM products
            {whereClause}
            ORDER BY ""Name""
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT COUNT(*) FROM products
            {whereClause};";

        var skip = (pageNumber - 1) * pageSize;

        using var multi = await _dbConnection.QueryMultipleAsync(
            new CommandDefinition(
                sql,
                new { Skip = skip, Take = pageSize, SearchTerm = $"%{searchTerm}%" },
                cancellationToken: cancellationToken))
            .ConfigureAwait(false);

        var products = (await multi.ReadAsync<Product>().ConfigureAwait(false)).ToList();
        var totalCount = await multi.ReadSingleAsync<int>().ConfigureAwait(false);

        return new PagedResult<Product>
        {
            Items = products,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// Implementação com EF Core para buscar uma única entidade.
    /// </summary>
    public async Task<Product?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(new object?[] { id, cancellationToken },
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para adicionar uma nova entidade.
    /// </summary>
    public async Task AddAsync(Product product,
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para atualizar uma entidade.
    /// </summary>
    public async Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para deletar uma entidade.
    /// </summary>
    public async Task DeleteAsync(Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para buscar múltiplas entidades por uma lista de IDs.
    /// </summary>    
    public async Task<List<Product>> GetByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}