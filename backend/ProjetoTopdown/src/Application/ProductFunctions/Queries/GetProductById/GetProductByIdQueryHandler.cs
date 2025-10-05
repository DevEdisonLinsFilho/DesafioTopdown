using MediatR;
using ProjetoTopdown.Application.Interfaces.Repositories;
using ProjetoTopdown.Application.ProductFunctions.Dtos;

namespace ProjetoTopdown.Application.ProductFunctions.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));        
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (product is null)
        {
            return null;
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Price = product.Price,
            StockQty = product.StockQty,
            IsActive = product.IsActive
        };
    }
}