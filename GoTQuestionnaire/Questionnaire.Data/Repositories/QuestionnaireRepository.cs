using Questionnaire.Domain.Model;

namespace Questionnaire.Data.Repositories;


public interface IQuestionnaireRepository
{
    Task<Questionnaire?> GetByIdAsync(int id);
}

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly QuestionnaireContext _context;

    public QuestionnaireRepository(QuestionnaireContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire?> GetByIdAsync(int id)
    {
        return await _context.Questionnaires.FindAsync(id);
    }
}