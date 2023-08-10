namespace QuestionnaireManager.Application.Commands.UpdateQuestionnaire;

public record UpdateQuestionnaireCommand(int Id, string Name, int MaxQuestions, int MaxAnswers) : ICommand;