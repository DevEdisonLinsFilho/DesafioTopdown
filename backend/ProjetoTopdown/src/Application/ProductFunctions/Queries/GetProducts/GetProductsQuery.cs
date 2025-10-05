using MediatR;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Application.ProductFunctions.Dtos;

namespace ProjetoTopdown.Application.ProductFunctions.Queries.GetProducts;

public record GetProductsQuery(int PageNumber, int PageSize, string? SearchTerm) 
    : IRequest<PagedResult<ProductDto>>;