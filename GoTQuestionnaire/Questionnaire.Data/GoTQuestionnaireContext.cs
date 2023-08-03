using Questionnaire.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Data;

public class QuestionnaireContext : DbContext
{
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}