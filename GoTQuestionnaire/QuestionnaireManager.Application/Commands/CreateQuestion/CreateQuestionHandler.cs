using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Infrastructure.Exceptions;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IAnswerRepository _answerRepository;

    public CreateQuestionHandler(IQuestionnaireRepository questionnaireRepository, IAnswerRepository answerRepository)
    {
        _questionnaireRepository = questionnaireRepository;
        _answerRepository = answerRepository;
    }

    public async Task<Result> HandleAsync(CreateQuestionCommand command)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(command.QuestionnaireId);
        if (questionnaire == null)
            return Result.Fail("Questionnaire not found");
        
        if (questionnaire.Questions.Count.Equals(questionnaire.MaxQuestions))
        {
            throw new QuestionsLimitReachedException();
        }

        if (!questionnaire.HasRoot)
        {
            questionnaire.HasRoot = true;
            questionnaire.Questions.Add(command.Question);
            await _questionnaireRepository.SaveChangesAsync();
            return Result.Ok();
        }

        var answer = await _answerRepository.GetByIdAsync(command.ParentAnswerId);
        
        if (answer == null)
            return Result.Fail("Answer not found");
        
        if (answer.ChildQuestion != null)
            return Result.Fail("Answer already have a question");

        answer.ChildQuestion = command.Question;
        await _answerRepository.SaveChangesAsync();

        questionnaire.Questions.Add(command.Question);
        await _questionnaireRepository.SaveChangesAsync();
        
        return Result.Ok();
    }
}