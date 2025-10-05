using FluentValidation;

namespace ProjetoTopdown.Application.ProductFunctions.Queries.GetProductById;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.")
            .GreaterThan(0).WithMessage("O ID do produto deve ser um número positivo.");
    }
}