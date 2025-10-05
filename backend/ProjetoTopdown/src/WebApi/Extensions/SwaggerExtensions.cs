using Microsoft.OpenApi.Models;
using ProjetoTopdown.Application.Models.SchemaFilters;
using ProjetoTopdown.WebApi.Configuration;
using ProjetoTopdown.WebApi.Filters;

namespace ProjetoTopdown.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(
        this IServiceCollection services,
        IConfiguration configurationManager)
    {
        if (configurationManager.GetValue<bool>(SwaggerSection.SwaggerEnabled))
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Projeto do desafio Topdown",
                    Version = "v1",
                    Description = "API para provação técnica do processo da TopDown"
                });                                
            });
        }
        RegisterSchemaFilters(services);
    }

    private static void RegisterSchemaFilters(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SchemaFilter<CreateProductCommandSchemaFilter>();
            options.SchemaFilter<UpdateProductCommandSchemaFilter>();
            options.SchemaFilter<CreateCustomerCommandSchemaFilter>();
            options.SchemaFilter<UpdateCustomerCommandSchemaFilter>();
            options.SchemaFilter<CreateOrderCommandSchemaFilter>();

            options.OperationFilter<IdempotencyKeyHeaderFilter>();
        });
    }

    public static void UseSwaggerConfiguration(
        this WebApplication app,
        IConfiguration configurationManager)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {            
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Tecnico Topdown API V1");
        });
    }
}
