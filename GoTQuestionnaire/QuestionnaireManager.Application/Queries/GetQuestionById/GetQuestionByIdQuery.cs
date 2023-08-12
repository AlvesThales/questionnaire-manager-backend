using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionById;

public record GetQuestionByIdQuery(int QuestionnaireId, int QuestionId) : IQuery<Question>;