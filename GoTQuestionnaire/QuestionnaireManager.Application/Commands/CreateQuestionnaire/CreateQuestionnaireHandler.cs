using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.CreateQuestionnaire;

public class CreateQuestionnaireHandler : ICommandHandler<CreateQuestionnaireCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public CreateQuestionnaireHandler(IQuestionnaireRepository questionnaireRepository)
    {
        _questionnaireRepository = questionnaireRepository;
    }

    public async Task<Result> HandleAsync(CreateQuestionnaireCommand command)
    {
       await _questionnaireRepository.AddAsync(command.Questionnaire);
       await _questionnaireRepository.SaveChangesAsync();
       return Result.Ok();
    }
}