using FluentValidation;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.");
    }
}