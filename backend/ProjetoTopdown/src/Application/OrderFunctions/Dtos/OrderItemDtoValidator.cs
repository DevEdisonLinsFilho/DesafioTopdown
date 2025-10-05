using FluentValidation;

namespace ProjetoTopdown.Application.OrderFunctions.Dtos;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(v => v.ProductId)
            .GreaterThan(0).WithMessage("O ID do produto é inválido.");

        RuleFor(v => v.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser no mínimo 1.");
    }
}