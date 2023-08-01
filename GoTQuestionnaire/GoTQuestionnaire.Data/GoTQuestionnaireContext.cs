using GoTQuestionnaire.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace GoTQuestionnaire.Data;

public class GoTQuestionnaireContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}