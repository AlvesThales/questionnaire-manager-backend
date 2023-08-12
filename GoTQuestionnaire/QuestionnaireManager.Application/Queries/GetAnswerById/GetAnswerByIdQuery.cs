using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetAnswerById;

public record GetAnswerByIdQuery(int Id) : IQuery<Answer>;