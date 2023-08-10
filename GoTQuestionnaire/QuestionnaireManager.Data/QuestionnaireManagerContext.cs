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
            .IsRequired(false);

        // Configure Question-Answer relationship
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.ParentQuestion)
            .HasForeignKey("ParentQuestionId")
            .IsRequired(false);
        
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Questionnaire)
            .WithMany(qn => qn.Questions)
            .HasForeignKey(q => q.QuestionnaireId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Answer-ChildQuestion relationship
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.ChildQuestion)
            .WithOne(q => q.ParentAnswer)
            .HasForeignKey<Question>("ParentAnswerId")
            .IsRequired(false);
    }
}