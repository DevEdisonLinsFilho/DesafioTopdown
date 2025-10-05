using MediatR;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var newCustomer = new Domain.Entities.Customer(
            request.Name,
            request.Email,
            request.Document);

        await _customerRepository.AddAsync(newCustomer, cancellationToken).ConfigureAwait(false);

        return newCustomer.Id;
    }
}