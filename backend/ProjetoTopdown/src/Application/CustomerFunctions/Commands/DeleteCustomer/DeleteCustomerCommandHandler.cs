using MediatR;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var customerToDelete = 
            await _customerRepository.GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (customerToDelete is null)
        {
            throw new NotFoundException($"Cliente com ID {request.Id} não foi encontrado.");
        }

        
        var hasOrders = await _customerRepository.HasOrdersAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (hasOrders)
        {        
            throw new ValidationException(
                "Não é possível deletar um cliente que possui pedidos associados.");
        }

        await _customerRepository.DeleteAsync(customerToDelete, cancellationToken)
            .ConfigureAwait(false);
    }
}