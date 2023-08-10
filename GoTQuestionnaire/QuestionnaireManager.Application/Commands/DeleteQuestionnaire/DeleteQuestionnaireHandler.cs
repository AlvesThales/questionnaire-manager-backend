using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.DeleteQuestionnaire;

public class DeleteQuestionnaireHandler : ICommandHandler<DeleteQuestionnaireCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public DeleteQuestionnaireHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<Result> HandleAsync(DeleteQuestionnaireCommand command)
    {
        return await _questionnaireRepository.DeleteAsync(command.Id);
    }
}