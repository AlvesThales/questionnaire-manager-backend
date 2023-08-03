using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Data.Repositories;


public interface IQuestionnaireRepository
{
    Task<Questionnaire?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly QuestionnaireManagerContext _context;

    public QuestionnaireRepository(QuestionnaireManagerContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire?> GetByIdAsync(int id)
    {
        return await _context.Questionnaires.FindAsync(id);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}