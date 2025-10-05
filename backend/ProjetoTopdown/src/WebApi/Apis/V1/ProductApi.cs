using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;
using ProjetoTopdown.Application.ProductFunctions.Commands.DeleteProduct;
using ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;
using ProjetoTopdown.Application.ProductFunctions.Dtos;
using ProjetoTopdown.Application.ProductFunctions.Queries.GetProductById;
using ProjetoTopdown.Application.ProductFunctions.Queries.GetProducts;
using ProjetoTopdown.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoTopdown.WebApi.Apis.V1;

#pragma warning disable MEN003 // Method is too long
public static class ProductApi
{
    private const string Version = "v1";

    public static void RegisterProductApiV1(this WebApplication app)
    {
        var group = app.MapGroup($"{Version}/products").WithTags("Products");

        // GET /v1/products
        group.MapGet("/",
            [ProducesResponseType(typeof(ApiResponse<PagedResult<ProductDto>>),
                (int)HttpStatusCode.OK)]
            [SwaggerOperation(Summary = "Lista os produtos de forma paginada.")]
        async (
                IMediator mediator,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] string? searchTerm = null) =>
            {
                var query = new GetProductsQuery(pageNumber, pageSize, searchTerm);
                var result = await mediator.Send(query).ConfigureAwait(false);

                return Results.Ok(ApiResponse<PagedResult<ProductDto>>.Success(result));
            });

        // GET /v1/products/{id}
        group.MapGet("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<ProductDto>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Busca um produto pelo seu ID.")]
        async (IMediator mediator, int id) =>
            {
                var query = new GetProductByIdQuery(id);
                var result = await mediator.Send(query).ConfigureAwait(false);
                
                if (result is null)
                {
                    throw new NotFoundException($"Produto com ID {id} não foi encontrado.");
                }

                return Results.Ok(ApiResponse<ProductDto>.Success(result));
            });

        // POST /v1/products
        group.MapPost("/",
            [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.Created)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
            [SwaggerOperation(Summary = "Cria um novo produto.")]
        async (IMediator mediator, CreateProductCommand createProductCommand) =>
            {
                var newProductId = await mediator.Send(createProductCommand).ConfigureAwait(false);
                var response = ApiResponse<int>.Success(
                    newProductId,
                    "Produto criado com sucesso.");

                return Results.Created($"/{Version}/products/{newProductId}", response);
            });

        // PUT /v1/products/{id}
        group.MapPut("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Atualiza um produto existente.")]
        async (IMediator mediator, int id, UpdateProductCommand updateProductCommand) =>
            {
                updateProductCommand.Id = id;
                await mediator.Send(updateProductCommand).ConfigureAwait(false);

                var response = ApiResponse<object>.Success("Produto atualizado com sucesso.");
                return Results.Ok(response);
            });

        // DELETE /v1/products/{id}
        group.MapDelete("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Deleta um produto existente.")]
        async (IMediator mediator, int id) =>
            {                
                var command = new DeleteProductCommand(id);
                await mediator.Send(command).ConfigureAwait(false);                
                return Results.Ok(ApiResponse<object>.Success("Produto deletado com sucesso."));
            });
    }
}