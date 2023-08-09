using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Commands.CreateQuestionnaire;

public record CreateQuestionnaireCommand(Questionnaire Questionnaire) : ICommand;