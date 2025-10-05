using MediatR;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Interfaces.Repositories;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        
        var productToDelete = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (productToDelete is null)
        {
            throw new NotFoundException($"Produto com ID {request.Id} não foi encontrado.");
        }
        
        await _productRepository.DeleteAsync(productToDelete, cancellationToken)
            .ConfigureAwait(false);
    }
}