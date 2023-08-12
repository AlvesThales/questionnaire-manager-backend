using QuestionnaireManager.Application.Queries.GetQuestionnaireById;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionnaires;

public class GetQuestionnairesHandler : IQueryHandlerAsync<GetQuestionnairesQuery, List<Questionnaire>>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public GetQuestionnairesHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<List<Questionnaire>> HandleAsync(GetQuestionnairesQuery query)
    {
        return await _questionnaireRepository.GetAllAsync();
    }
}