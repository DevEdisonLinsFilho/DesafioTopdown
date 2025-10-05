using FluentValidation;
using ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(200).WithMessage("O nome não pode exceder 200 caracteres.");

        RuleFor(v => v.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O formato do email é inválido.")
            .MaximumLength(200).WithMessage("O email não pode exceder 200 caracteres.");

        RuleFor(v => v.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .MaximumLength(50).WithMessage("O documento não pode exceder 50 caracteres.");
    }
}