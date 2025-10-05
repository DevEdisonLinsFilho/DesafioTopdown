using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProjetoTopdown.Application.CustomerFunctions.Commands.CreateCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Commands.DeleteCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Commands.UpdateCustomer;
using ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomerById;
using ProjetoTopdown.Application.CustomerFunctions.Queries.GetCustomers;
using ProjetoTopdown.Application.ProductFunctions.Commands.CreateProduct;
using ProjetoTopdown.Application.ProductFunctions.Commands.DeleteProduct;
using ProjetoTopdown.Application.ProductFunctions.Commands.UpdateProduct;
using ProjetoTopdown.Application.ProductFunctions.Queries.GetProductById;
using ProjetoTopdown.Application.ProductFunctions.Queries.GetProducts;

namespace ProjetoTopdown.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupCQRS(this IServiceCollection services)
    {
        AddValidators(services);
        AddCommandHandlers(services);
    }

    public static void SetupApplicationServices(this IServiceCollection services)
    {        
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Layout",
        "MEN003:Method is too long",
        Justification = "No point in separating validators")]
    private static void AddValidators(this IServiceCollection services)
    {

        services.AddScoped<
            AbstractValidator<CreateProductCommand>,
            CreateProductCommandValidator>();
        
        services.AddScoped<
            AbstractValidator<UpdateProductCommand>,
            UpdateProductCommandValidator>();

        services.AddScoped<
            AbstractValidator<DeleteProductCommand>,
            DeleteProductCommandValidator>();

        services.AddScoped<
            AbstractValidator<CreateProductCommand>,
            CreateProductCommandValidator>();

        services.AddScoped<
            AbstractValidator<GetProductByIdQuery>,
            GetProductByIdQueryValidator>();
        
        services.AddScoped<
            AbstractValidator<GetProductsQuery>,
            GetProductsQueryValidator>();

        services.AddScoped<
            AbstractValidator<CreateCustomerCommand>,
            CreateCustomerCommandValidator>();

        services.AddScoped<
            AbstractValidator<UpdateCustomerCommand>,
            UpdateCustomerCommandValidator>();

        services.AddScoped<
            AbstractValidator<DeleteCustomerCommand>,
            DeleteCustomerCommandValidator>();

        services.AddScoped<
            AbstractValidator<CreateCustomerCommand>,
            CreateCustomerCommandValidator>();

        services.AddScoped<
            AbstractValidator<GetCustomerByIdQuery>,
            GetCustomerByIdQueryValidator>();

        services.AddScoped<
            AbstractValidator<GetCustomersQuery>,
            GetCustomersQueryValidator>();

    }

    private static void AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateProductCommandHandler>();
        services.AddScoped<UpdateProductCommandHandler>();
        services.AddScoped<DeleteProductCommandHandler>();
        services.AddScoped<GetProductByIdQueryHandler>();
        services.AddScoped<GetProductsQueryHandler>();

        services.AddScoped<CreateCustomerCommandHandler>();
        services.AddScoped<UpdateCustomerCommandHandler>();
        services.AddScoped<DeleteCustomerCommandHandler>();
        services.AddScoped<GetCustomerByIdQueryHandler>();
        services.AddScoped<GetCustomersQueryHandler>();
    }
}
