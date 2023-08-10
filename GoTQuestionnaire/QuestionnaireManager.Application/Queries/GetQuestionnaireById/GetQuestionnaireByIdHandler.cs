using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionnaireById;

public class GetQuestionnaireByIdHandler : IQueryHandlerAsync<GetQuestionnaireByIdQuery, Questionnaire>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public GetQuestionnaireByIdHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<Questionnaire?> HandleAsync(GetQuestionnaireByIdQuery query)
    {
        return await _questionnaireRepository.GetByIdAsync(query.Id);
    }
}