namespace QuestionnaireManager.Application.Commands.CreateAnswer;

public record CreateAnswerCommand(int QuestionnaireId, int ParentQuestionId, string Description) : ICommand;