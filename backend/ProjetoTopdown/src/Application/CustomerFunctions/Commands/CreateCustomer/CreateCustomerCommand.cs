using MediatR;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
}