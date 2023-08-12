using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionnaires;

public record GetQuestionnairesQuery() : IQuery<List<Questionnaire>>;
