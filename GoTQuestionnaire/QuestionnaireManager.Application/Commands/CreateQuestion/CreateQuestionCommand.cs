using QuestionnaireManager.Domain.Model;

namespace QuestionnaireManager.Application.Commands.CreateQuestion;

public record CreateQuestionCommand(int QuestionnaireId, int ParentAnswerId, Question Question) : ICommand;