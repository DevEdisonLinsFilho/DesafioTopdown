using MediatR;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        var productToUpdate = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (productToUpdate is null)
        {
            throw new NotFoundException($"Produto com ID {request.Id} não foi encontrado.");
        }

        productToUpdate.Update(
            request.Name,
            request.Price,
            request.StockQty,
            request.IsActive);
        
        await _productRepository.UpdateAsync(
            productToUpdate,
            cancellationToken)
        .ConfigureAwait(false);
    }
}