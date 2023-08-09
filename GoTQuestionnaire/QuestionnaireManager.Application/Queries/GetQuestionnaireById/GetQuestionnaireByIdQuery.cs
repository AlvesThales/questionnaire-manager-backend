using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionnaireById;

public record GetQuestionnaireByIdQuery(int Id) : IQuery<Questionnaire>;
