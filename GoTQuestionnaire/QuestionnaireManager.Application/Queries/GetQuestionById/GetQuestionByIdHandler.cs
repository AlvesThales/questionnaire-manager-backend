using QuestionnaireManager.Application.Queries.GetQuestions;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionById;

public class GetQuestionByIdHandler : IQueryHandlerAsync<GetQuestionByIdQuery, Question>
{

    private readonly IQuestionRepository _questionRepository;

    public GetQuestionByIdHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question> HandleAsync(GetQuestionByIdQuery query)
    {
        return await _questionRepository.GetByIdAsync(query.Id);
        
    }
}