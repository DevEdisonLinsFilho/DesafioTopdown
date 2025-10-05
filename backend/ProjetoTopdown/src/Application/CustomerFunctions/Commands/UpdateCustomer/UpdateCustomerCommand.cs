using MediatR;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
}