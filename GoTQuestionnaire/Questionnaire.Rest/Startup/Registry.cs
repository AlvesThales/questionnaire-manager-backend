using Questionnaire.Rest.Controllers.Utils;

namespace Questionnaire.Rest.Startup;

public static class Registry
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMediator, Mediator>();

    }

}