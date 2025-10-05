using ProjetoTopdown.WebApi.Apis.V1;
using ProjetoTopdown.WebApi.Enums;

namespace ProjetoTopdown.WebApi.Apis;

public static class ApiVersions
{
    public static void ConfigureApi(this WebApplication app, ApiVersion version)
    {
        switch (version)
        {
            case ApiVersion.V1:
                app.RegisterProductApiV1();
                app.RegisterCustomerApiV1();
                app.RegisterOrderApiV1();
                break;
        }
    }
}
