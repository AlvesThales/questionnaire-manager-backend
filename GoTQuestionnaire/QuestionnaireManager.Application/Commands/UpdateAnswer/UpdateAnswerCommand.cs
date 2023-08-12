namespace QuestionnaireManager.Application.Commands.UpdateAnswer;

public record UpdateAnswerCommand(int Id, string Description) : ICommand;