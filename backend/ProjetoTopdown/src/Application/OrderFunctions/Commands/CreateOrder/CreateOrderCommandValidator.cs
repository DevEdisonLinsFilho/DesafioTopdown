using FluentValidation;
using ProjetoTopdown.Application.OrderFunctions.Dtos;

namespace ProjetoTopdown.Application.OrderFunctions.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(v => v.CustomerId)
            .GreaterThan(0).WithMessage("O ID do cliente é inválido.");

        RuleFor(v => v.Items)
            .NotEmpty().WithMessage("O pedido deve conter pelo menos um item.");

        RuleForEach(v => v.Items)
            .SetValidator(new OrderItemDtoValidator());
    }
}