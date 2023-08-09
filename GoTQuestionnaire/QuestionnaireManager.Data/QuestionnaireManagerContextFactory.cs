using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace QuestionnaireManager.Data
{
    public class QuestionnaireManagerContextFactory : IDesignTimeDbContextFactory<QuestionnaireManagerContext>
    {
        public QuestionnaireManagerContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + "\\..\\QuestionnaireManager.Rest";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<QuestionnaireManagerContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("QuestionnaireDatabase"));

            return new QuestionnaireManagerContext(optionsBuilder.Options);
        }
    }
}