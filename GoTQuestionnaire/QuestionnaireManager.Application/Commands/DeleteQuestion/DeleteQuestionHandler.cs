using QuestionnaireManager.Application.Commands.DeleteQuestionnaire;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.DeleteQuestion;

public class DeleteQuestionHandler : ICommandHandler<DeleteQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;

    public DeleteQuestionHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Result> HandleAsync(DeleteQuestionCommand command)
    {
        return await _questionRepository.DeleteAsync(command.Id);
    }
}