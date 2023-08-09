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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Questionnaire-Question relationship
        modelBuilder.Entity<Questionnaire>()
            .HasMany(q => q.Questions)
            .WithOne()
            .HasForeignKey("QuestionnaireId");

        // Configure Question-Answer relationship
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.ParentQuestion)
            .HasForeignKey("ParentQuestionId");

        // Configure Answer-ChildQuestion relationship
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.ChildQuestion)
            .WithOne(q => q.ParentAnswer)
            .HasForeignKey<Question>("AnswerId");
    }
}