using QuestionnaireManager.Application.Commands.CreateQuestion;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.CreateRootQuestion;

public class CreateRootQuestionHandler : ICommandHandler<CreateRootQuestionCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public CreateRootQuestionHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<Result> HandleAsync(CreateRootQuestionCommand command)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(command.QuestionnaireId);
        if (questionnaire == null)
            return Result.Fail("Questionnaire not found");
        
        var existingRootQuestion = questionnaire.Questions.SingleOrDefault(q => q.IsRoot);
        if (existingRootQuestion != null)
            questionnaire.Questions.Remove(existingRootQuestion);
        
        var question = new Question(command.Description) { IsRoot = true };
        questionnaire.HasRoot = true;
        questionnaire.Questions.Clear();
        questionnaire.Questions.Add(question);
        await _questionnaireRepository.SaveChangesAsync();
        return Result.Ok();
    }
}