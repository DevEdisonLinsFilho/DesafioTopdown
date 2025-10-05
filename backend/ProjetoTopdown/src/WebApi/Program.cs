using ProjetoTopdown.WebApi;
using ProjetoTopdown.WebApi.Apis;
using ProjetoTopdown.WebApi.Enums;
using System.Diagnostics.CodeAnalysis;

namespace WebApi;

[ExcludeFromCodeCoverage]
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.RegisterServices();

        var app = builder.Build();

        app.Configure(builder);

        app.ConfigureApi(ApiVersion.V1);

        app.MapGet("/", () => Results.Redirect("/swagger"));

        app.Run();
    }
}