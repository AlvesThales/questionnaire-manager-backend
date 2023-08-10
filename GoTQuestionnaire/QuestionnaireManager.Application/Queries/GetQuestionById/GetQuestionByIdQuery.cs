using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestionById;

public record GetQuestionByIdQuery(int Id) : IQuery<Question>;