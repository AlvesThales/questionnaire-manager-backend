using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Queries.GetQuestions;

public record GetQuestionByIdQuery(int Id) : IQuery<Question>;