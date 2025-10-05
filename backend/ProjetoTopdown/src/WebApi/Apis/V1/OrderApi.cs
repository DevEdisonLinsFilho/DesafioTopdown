using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder;
using ProjetoTopdown.WebApi.Attributes;
using ProjetoTopdown.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProjetoTopdown.WebApi.Apis.V1;

public static class OrderApi
{
    private const string Version = "v1";

    public static void RegisterOrderApiV1(this WebApplication app)
    {
        var group = app.MapGroup($"{Version}/orders").WithTags("Orders");

        // POST /v1/orders
        group.MapPost("/",
            [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), (int)HttpStatusCode.NotFound)]
        [SwaggerOperation(Summary = "Cria um novo pedido com idempotência.")]
        async (
            IMediator mediator,
            HttpContext httpContext,
            [FromBody] CreateOrderCommand createOrderCommand) =>
            {
                if (!httpContext.Request.Headers.TryGetValue(
                    "Idempotency-Key",
                    out var idempotencyKeyStr) ||
                    !Guid.TryParse(idempotencyKeyStr, out var idempotencyKey))
                {
                    return Results.BadRequest(
                        ApiResponse.Error("O header 'Idempotency-Key' é obrigatório " +
                        "e deve ser um GUID válido."));
                }

                createOrderCommand.IdempotencyKey = idempotencyKey;

                var newOrderId = await mediator.Send(createOrderCommand).ConfigureAwait(false);

                var response = ApiResponse<int>.Success(newOrderId, "Pedido criado com sucesso.");
                return Results.Created($"/{Version}/orders/{newOrderId}", response);
            }).WithMetadata(new IdempotencyKeyRequiredAttribute());
        ;
    }
}