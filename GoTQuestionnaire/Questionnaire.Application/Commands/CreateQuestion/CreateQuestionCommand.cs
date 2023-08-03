using Questionnaire.Domain.Model;

namespace Questionnaire.Application.Commands.CreateQuestion;

public record CreateQuestionCommand(Question Question) : ICommand;