using QuestionnaireManager.Application.Commands.CreateQuestion;
using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Exceptions;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.CreateAnswer;

public class CreateAnswerHandler : ICommandHandler<CreateAnswerCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;

    public CreateAnswerHandler(IQuestionnaireRepository questionnaireRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }

    public async Task<Result> HandleAsync(CreateAnswerCommand command)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(command.QuestionnaireId);
        if (questionnaire == null)
            return Result.Fail("Questionnaire not found");

        var question = await _questionRepository.GetByIdAsync(command.ParentQuestionId);
        if (question == null)
            return Result.Fail("Question not found");
        
        if (question.Answers.Count.Equals(questionnaire.MaxAnswers))
        {
            throw new AnswersLimitReachedException();
        }

        var answer = new Answer(command.Description)
        {
            ParentQuestionId = command.ParentQuestionId
        };

        question.Answers.Add(answer);
        await _questionRepository.SaveChangesAsync();
        
        return Result.Ok();
    }
}