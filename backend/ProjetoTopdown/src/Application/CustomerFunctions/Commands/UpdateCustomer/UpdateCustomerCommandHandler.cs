using MediatR;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {

        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var customerToUpdate = await _customerRepository.GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (customerToUpdate is null)
        {
            throw new NotFoundException($"Cliente com ID {request.Id} não foi encontrado.");
        }

        customerToUpdate.Update(
            request.Name,
            request.Email,
            request.Document);

        await _customerRepository.UpdateAsync(customerToUpdate, cancellationToken)
            .ConfigureAwait(false);
    }
}