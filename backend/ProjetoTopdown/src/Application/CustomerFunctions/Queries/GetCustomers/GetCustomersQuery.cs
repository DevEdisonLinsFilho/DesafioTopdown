using MediatR;
using ProjetoTopdown.Application.CustomerFunctions.Dtos;
using ProjetoTopdown.Application.Models;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;

/// <summary>
/// Query para buscar uma lista paginada de clientes.
/// </summary>
/// <param name="PageNumber">O número da página a ser retornada.</param>
/// <param name="PageSize">A quantidade de itens por página.</param>
public record GetCustomersQuery(int PageNumber, int PageSize, string? SearchTerm)
    : IRequest<PagedResult<CustomerDto>>;