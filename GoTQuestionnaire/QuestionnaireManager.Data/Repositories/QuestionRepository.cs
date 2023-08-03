using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Data.Repositories;

public class QuestionRepository : IQuestionRepository
{
    public Task<Question> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}

public interface IQuestionRepository
{
    Task<Question> GetByIdAsync(int id);
}