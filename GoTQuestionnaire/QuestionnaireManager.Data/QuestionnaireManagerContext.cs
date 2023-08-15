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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Questionnaire-Question relationship
        modelBuilder.Entity<Questionnaire>()
            .HasMany(q => q.Questions)
            .WithOne()
            .HasForeignKey("QuestionnaireId")
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Question-Answer relationship
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey("ParentQuestionId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Answer-ChildQuestion relationship
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.ChildQuestion)
            .WithOne()
            .HasForeignKey<Question>("ParentAnswerId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}