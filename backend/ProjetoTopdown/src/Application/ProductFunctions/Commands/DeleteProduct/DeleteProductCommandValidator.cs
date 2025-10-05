using FluentValidation;

namespace ProjetoTopdown.Application.ProductFunctions.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");
    }
}