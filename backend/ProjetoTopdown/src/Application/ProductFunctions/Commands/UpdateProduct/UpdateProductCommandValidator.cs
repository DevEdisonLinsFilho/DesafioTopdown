using FluentValidation;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(v => v.StockQty)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");
    }
}