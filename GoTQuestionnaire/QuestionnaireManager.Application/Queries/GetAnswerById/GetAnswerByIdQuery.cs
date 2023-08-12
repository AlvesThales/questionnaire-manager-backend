using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetAnswerById;

public record GetAnswerByIdQuery(int QuestionnaireId, int QuestionId, int AnswerId) : IQuery<Answer>;