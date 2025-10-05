using MediatR;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Application.ProductFunctions.Dtos;

namespace ProjetoTopdown.Application.ProductFunctions.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedResult<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var pagedResult = await _productRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SearchTerm,
            cancellationToken)
        .ConfigureAwait(false);
        
        var productDtos = pagedResult.Items.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Sku = p.Sku,
            Price = p.Price,
            StockQty = p.StockQty,
            IsActive = p.IsActive
        }).ToList();

        return new PagedResult<ProductDto>
        {
            Items = productDtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize
        };
    }
}