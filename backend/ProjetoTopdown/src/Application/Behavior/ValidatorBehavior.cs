using FluentValidation;
using MediatR;

namespace ProjetoTopdown.Application.Behaviour;

#pragma warning disable CA2016 // Encaminhe o parâmetro 'CancellationToken' para os métodos
#pragma warning disable CA2007 // Considere chamar ConfigureAwait na tarefa esperada
#pragma warning disable CA1062

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new Exceptions.ValidationApplicationException(typeof(TRequest).Name, failures);
        }


        return await next();
    }
}
