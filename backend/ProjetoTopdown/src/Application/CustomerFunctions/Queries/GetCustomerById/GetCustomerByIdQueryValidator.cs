using FluentValidation;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomerById;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.")
            .GreaterThan(0).WithMessage("O ID do cliente deve ser um número positivo.");
    }
}