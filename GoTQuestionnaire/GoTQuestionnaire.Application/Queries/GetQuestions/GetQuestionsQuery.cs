using GoTQuestionnaire.Domain.Model;

namespace GoTQuestionnaire.Application.Queries.GetQuestions;

public record GetQuestionsQuery(Question Question) : IQuery<Question>;