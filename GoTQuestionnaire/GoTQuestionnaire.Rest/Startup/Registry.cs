using GoTQuestionnaire.Rest.Controllers.Utils;

namespace GoTQuestionnaire.Rest.Startup;

public static class Registry
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMediator, Mediator>();

    }

}