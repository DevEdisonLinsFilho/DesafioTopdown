using FluentValidation;
using ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;

namespace ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;

public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
{
    public GetCustomersQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("O número da página deve ser no mínimo 1.");

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("O tamanho da página deve ser no mínimo 1.")
            .LessThanOrEqualTo(100).WithMessage("O tamanho máximo da página é de 100 itens.");
    }
}