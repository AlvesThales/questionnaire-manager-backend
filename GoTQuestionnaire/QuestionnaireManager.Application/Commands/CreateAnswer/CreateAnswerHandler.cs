using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Infrastructure.Utils;

namespace QuestionnaireManager.Application.Commands.CreateAnswer;

public class CreateAnswerHandler : ICommandHandler<CreateAnswerCommand>
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;

    public CreateAnswerHandler(IQuestionnaireRepository questionnaireRepository, IQuestionRepository questionRepository)
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
    }

    public async Task<Result> HandleAsync(CreateAnswerCommand command)
    {
        var questionnaire = await _questionnaireRepository.GetByIdAsync(command.QuestionnaireId);
        if (questionnaire?.Questions == null)
            return Result.Fail("Questionnaire not found or contains no questions");

        var question = questionnaire.Questions.FirstOrDefault(q => q.Id == command.ParentQuestionId);
        if (question == null)
            return Result.Fail("Question not found");
        
        if (question.Answers.Count.Equals(questionnaire.MaxAnswers))
        {
            return Result.Fail("Answers limit has been reached");
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