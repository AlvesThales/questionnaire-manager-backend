using Microsoft.EntityFrameworkCore;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Data;

public class QuestionnaireManagerContext : DbContext
{
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }


    public QuestionnaireManagerContext(DbContextOptions<QuestionnaireManagerContext> options)
        : base(options)
    {

    }
}