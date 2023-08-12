using QuestionnaireManager.Application.Commands.UpdateQuestion;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.UpdateAnswer;

public class UpdateAnswerHandler : ICommandHandler<UpdateAnswerCommand>
{
    private readonly IAnswerRepository _answerRepository;

    public UpdateAnswerHandler(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Result> HandleAsync(UpdateAnswerCommand command)
    {
        return await _answerRepository.UpdateAsync(command.Id, command.Description);
    }
}