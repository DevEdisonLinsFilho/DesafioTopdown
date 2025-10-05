using MediatR;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;