using FluentValidation;

namespace ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

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