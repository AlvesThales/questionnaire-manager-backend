using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Data.Repositories;

public interface IAnswerRepository
{
    Task<Answer?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}

public class AnswerRepository : IAnswerRepository
{
    private readonly QuestionnaireManagerContext _context;

    public AnswerRepository(QuestionnaireManagerContext context)
    {
        _context = context;
    }

    public async Task<Answer?> GetByIdAsync(int id)
    {
        return await _context.Answers.FindAsync(id);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}