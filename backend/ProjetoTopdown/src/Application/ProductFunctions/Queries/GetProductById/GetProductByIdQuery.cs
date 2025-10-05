using MediatR;
using ProjetoTopdown.Application.ProductFunctions.Dtos;

namespace ProjetoTopdown.Application.ProductFunctions.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;