using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetAnswerById;

public class GetAnswerByIdHandler : IQueryHandlerAsync<GetAnswerByIdQuery, Answer>
{

    private readonly IAnswerRepository _answerRepository;

    public GetAnswerByIdHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Answer?> HandleAsync(GetAnswerByIdQuery query)
    {
        return await _answerRepository.GetByIdAsync(query.AnswerId);
    }
}