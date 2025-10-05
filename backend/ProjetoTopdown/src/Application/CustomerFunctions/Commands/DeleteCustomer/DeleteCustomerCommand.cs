using MediatR;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.DeleteCustomer;

/// <summary>
/// Command para deletar um cliente pelo seu ID.
/// </summary>
/// <param name="Id">O ID do cliente a ser deletado.</param>
public record DeleteCustomerCommand(int Id) : IRequest;