using ProjetoTopdown.Application.Extensions;
using ProjetoTopdown.Infrastructure.Extensions;
using ProjetoTopdown.WebApi.Configuration;
using ProjetoTopdown.WebApi.Extensions;
using ProjetoTopdown.WebApi.Middlewares;

namespace ProjetoTopdown.WebApi;

public static class WebAppBuilderExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);        

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.SetupApplicationServices();
        builder.Services.SetupCQRS();
        builder.Services.AddMediator();

        builder.Services.AddSwagger(builder.Configuration);
        builder.Services.AddHttpContextAccessor();

        LoadInfrastructureDependencies(builder);        
    }

    public static void Configure(this WebApplication app, WebApplicationBuilder builder)
    { 
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(app);

        app.UseExceptionHandler(app.Environment, app.Logger);

        app.UseHttpsRedirection();

        if (builder.Configuration.GetValue<bool>(SwaggerSection.SwaggerEnabled))
        {
            app.UseSwaggerConfiguration(builder.Configuration);
        }        
    }

    private static void LoadInfrastructureDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddHttpClient();
        builder.Services.AddDbRepositories(builder.Configuration!);
    }
}
