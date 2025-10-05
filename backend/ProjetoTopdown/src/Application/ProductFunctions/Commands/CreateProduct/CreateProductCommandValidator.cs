using FluentValidation;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");

        RuleFor(v => v.Sku)
            .NotEmpty().WithMessage("O SKU é obrigatório.")
            .MaximumLength(50).WithMessage("O SKU não pode exceder 50 caracteres.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(v => v.StockQty)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");
    }
}