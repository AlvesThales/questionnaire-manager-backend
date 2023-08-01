using GoTQuestionnaire.Domain.Model;

namespace GoTQuestionnaire.Application.Commands.CreateQuestion;

public record CreateQuestionCommand(Question Question) : ICommand;