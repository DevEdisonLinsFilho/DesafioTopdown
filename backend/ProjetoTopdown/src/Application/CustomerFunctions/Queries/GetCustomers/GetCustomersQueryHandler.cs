using MediatR;
using ProjetoTopdown.Application.CustomerFunctions.Dtos;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.Models;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedResult<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<PagedResult<CustomerDto>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var pagedResult = await _customerRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SearchTerm,
            cancellationToken)
        .ConfigureAwait(false);

        var customerDtos = pagedResult.Items.Select(c => new CustomerDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Document = c.Document
        }).ToList();

        return new PagedResult<CustomerDto>
        {
            Items = customerDtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }
}