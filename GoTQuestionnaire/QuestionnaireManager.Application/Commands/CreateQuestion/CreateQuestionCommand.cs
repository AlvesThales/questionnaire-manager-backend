namespace QuestionnaireManager.Application.Commands.CreateQuestion;

public record CreateQuestionCommand(int QuestionnaireId, int ParentAnswerId, string Description) : ICommand;