using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Commands.DeleteCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Dtos;
using ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomerById;
using ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;
using ProjetoTopdown.Application.Exceptions;
using ProjetoTopdown.Application.Models;
using ProjetoTopdown.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoTopdown.WebApi.Apis.V1;

public static class CustomerApi
{
    private const string Version = "v1";

#pragma warning disable MEN003 // Method is too long
    public static void RegisterCustomerApiV1(this WebApplication app)
    {
        var group = app.MapGroup($"{Version}/customers").WithTags("Customers");

        // GET /v1/customers
        group.MapGet("/",
            [ProducesResponseType(typeof(ApiResponse<PagedResult<CustomerDto>>),
            (int)HttpStatusCode.OK)]
            [SwaggerOperation(Summary = "Lista os clientes de forma paginada.")]
        async (
                IMediator mediator,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] string? searchTerm = null) =>
            {
                var query = new GetCustomersQuery(pageNumber, pageSize, searchTerm);
                var result = await mediator.Send(query).ConfigureAwait(false);
                return Results.Ok(ApiResponse<PagedResult<CustomerDto>>.Success(result));
            });

        // GET /v1/customers/{id}
        group.MapGet("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<CustomerDto>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<GetCustomerByIdQuery>),
                (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Busca um cliente pelo seu ID.")]
        async (IMediator mediator, int id) =>
            {
                var query = new GetCustomerByIdQuery(id);
                var result = await mediator.Send(query).ConfigureAwait(false);
                if (result is null)
                    throw new NotFoundException($"Cliente com ID {id} não foi encontrado.");
                return Results.Ok(ApiResponse<CustomerDto>.Success(result));
            });

        // POST /v1/customers
        group.MapPost("/",
            [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.Created)]
            [ProducesResponseType(typeof(ApiResponse<CreateCustomerCommand>),
                (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Summary = "Cria um novo cliente.")]
        async (IMediator mediator, CreateCustomerCommand createCustomerCommand) =>
            {
                var newCustomerId = 
                    await mediator.Send(createCustomerCommand).ConfigureAwait(false);

                var response = ApiResponse<int>.Success(
                    newCustomerId,
                    "Cliente criado com sucesso.");
                return Results.Created($"/{Version}/customers/{newCustomerId}", response);
            });

        // PUT /v1/customers/{id}
        group.MapPut("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<UpdateCustomerCommand>),
                (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<UpdateCustomerCommand>),
                (int)HttpStatusCode.BadRequest)]
            [ProducesResponseType(typeof(ApiResponse<UpdateCustomerCommand>),
                (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Atualiza um cliente existente.")]
        async (IMediator mediator, int id, UpdateCustomerCommand updateCustomerCommand) =>
            {
                updateCustomerCommand.Id = id;
                await mediator.Send(updateCustomerCommand).ConfigureAwait(false);
                var response = ApiResponse<object>.Success("Cliente atualizado com sucesso.");
                return Results.Ok(response);
            });

        // DELETE /v1/customers/{id}
        group.MapDelete("/{id:int}",
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.OK)]
            [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
            [SwaggerOperation(Summary = "Deleta um cliente existente.")]
        async (IMediator mediator, int id) =>
            {
                var command = new DeleteCustomerCommand(id);
                await mediator.Send(command).ConfigureAwait(false);
                return Results.Ok(ApiResponse<object>.Success("Cliente deletado com sucesso."));
            });
    }
}