using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.UpdateQuestionnaire;

public class UpdateQuestionnaireHandler : ICommandHandler<UpdateQuestionnaireCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public UpdateQuestionnaireHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<Result> HandleAsync(UpdateQuestionnaireCommand command)
    {
        return await _questionnaireRepository.UpdateAsync(command.Id, command.Name, command.MaxQuestions, command.MaxAnswers);
    }
}