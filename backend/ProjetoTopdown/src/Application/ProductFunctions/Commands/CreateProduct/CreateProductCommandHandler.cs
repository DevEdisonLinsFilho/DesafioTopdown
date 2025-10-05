using MediatR;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var newProduct = new Domain.Entities.Product(
            request.Name,
            request.Sku,
            request.Price,
            request.StockQty);
        
        await _productRepository.AddAsync(newProduct, cancellationToken).ConfigureAwait(false);

        return newProduct.Id;
    }
}