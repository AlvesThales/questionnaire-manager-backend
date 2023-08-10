namespace QuestionnaireManager.Application.Commands.CreateRootQuestion;

public record CreateRootQuestionCommand(int QuestionnaireId, string Description) : ICommand;