using GoTQuestionnaire.Data.Repositories;
using GoTQuestionnaire.Domain.Model;
using GoTQuestionnaire.Infrastructure.Utils;

namespace GoTQuestionnaire.Application.Commands.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;
    public Task<Result> HandleAsync(CreateQuestionCommand command)
    {
        throw new NotImplementedException();
    }
}