using Dapper;
using Microsoft.EntityFrameworkCore;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Domain.Entities;
using System.Data;

namespace ProjetoTopdown.Infrastructure.Persistence.Repositories.CustomerPersistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IDbConnection _dbConnection;

    public CustomerRepository(ApplicationDbContext context, IDbConnection dbConnection)
    {
        _context = context;
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Implementação com Dapper para leitura paginada de clientes.
    /// </summary>
    public async Task<PagedResult<Customer>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm,
        CancellationToken cancellationToken)
    {
        var whereClause = "";
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            whereClause = @"WHERE ""Name"" ILIKE @SearchTerm OR ""Email"" ILIKE @SearchTerm";
        }

        var sql = $@"
            SELECT * FROM customers
            {whereClause}
            ORDER BY ""Name""
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

            SELECT COUNT(*) FROM customers
            {whereClause};";

        var skip = (pageNumber - 1) * pageSize;

        using var multi = await _dbConnection.QueryMultipleAsync(
             new CommandDefinition(
                 sql,
                 new { Skip = skip, Take = pageSize, SearchTerm = $"%{searchTerm}%" },
                 cancellationToken: cancellationToken))
             .ConfigureAwait(false);

        var customers = (await multi.ReadAsync<Customer>().ConfigureAwait(false)).ToList();
        var totalCount = await multi.ReadSingleAsync<int>().ConfigureAwait(false);

        return new PagedResult<Customer>
        {
            Items = customers,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// Implementação com EF Core para buscar um único cliente.
    /// </summary>
    public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .FindAsync(new object[] { id }, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para adicionar um novo cliente.
    /// </summary>
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken).ConfigureAwait(false);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para atualizar um cliente.
    /// </summary>
    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Implementação com EF Core para deletar um cliente.
    /// </summary>
    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        }
        catch (Exception ex)
        {
#pragma warning disable CA2200 // Gerar novamente para preservar detalhes da pilha
            throw ex;
#pragma warning restore CA2200 // Gerar novamente para preservar detalhes da pilha
        }
    }

    public async Task<bool> HasOrdersAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AnyAsync(
            o => o.CustomerId == customerId, cancellationToken).ConfigureAwait(false);
    }
}