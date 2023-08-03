using Questionnaire.Domain.Model;

namespace Questionnaire.Application.Queries.GetQuestions;

public record GetQuestionsQuery(Question Question) : IQuery<Question>;