using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.UpdateQuestion;

public class UpdateQuestionHandler : ICommandHandler<UpdateQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public UpdateQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Result> HandleAsync(UpdateQuestionCommand command)
    {
        return await _questionRepository.UpdateAsync(command.Id, command.Description);
    }
}