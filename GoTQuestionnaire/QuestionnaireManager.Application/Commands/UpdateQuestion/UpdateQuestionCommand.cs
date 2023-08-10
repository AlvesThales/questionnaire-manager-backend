namespace QuestionnaireManager.Application.Commands.UpdateQuestion;

public record UpdateQuestionCommand(int Id, string Description) : ICommand;