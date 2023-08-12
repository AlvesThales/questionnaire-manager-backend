using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.DeleteAnswer;

public class DeleteAnswerHandler : ICommandHandler<DeleteAnswerCommand>
{
    private readonly IAnswerRepository _answerRepository;

    public DeleteAnswerHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result> HandleAsync(DeleteAnswerCommand command)
    {
        return await _answerRepository.DeleteAsync(command.Id);
    }
}