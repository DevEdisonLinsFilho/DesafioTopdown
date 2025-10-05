using MediatR;
using ProjetoTopdown.Application.CustomerFunctions.Dtos;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomerById;

#pragma warning disable CA1508 // Evite um código condicional morto
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto?> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        ProjetoTopdown.Domain.Entities.Customer? customer = await _customerRepository.GetByIdAsync(
            request.Id,
            cancellationToken)        
        .ConfigureAwait(false);

        if (customer is null)
        {
            return null;
        }

        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Document = customer.Document
        };
    }
}