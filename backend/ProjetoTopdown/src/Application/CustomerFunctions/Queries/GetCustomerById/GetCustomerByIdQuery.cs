using MediatR;
using ProjetoTopdown.Application.CustomerFunctions.Dtos;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomerById;

/// <summary>
/// Query para buscar um cliente específico pelo seu ID.
/// </summary>
/// <param name="Id">O ID do cliente a ser buscado.</param>
public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto?>;