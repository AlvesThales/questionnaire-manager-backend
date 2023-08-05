using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Data;
using QuestionnaireManager.Rest.Controllers.Utils;

namespace QuestionnaireManager.Rest.Startup;

public static class Registry
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMediator, Mediator>();
        
        services.AddDbContext<QuestionnaireManagerContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("QuestionnaireDatabase"))
        );
    }
}