using FluentValidation;
using MediatR;
using ProjetoTopdown.Application.Behaviour;

namespace ProjetoTopdown.WebApi.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining(typeof(ValidatorBehavior<,>)));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddValidatorsFromAssemblyContaining(typeof(ValidatorBehavior<,>));
    }
}
