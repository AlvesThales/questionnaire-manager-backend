using QuestionnaireManager.Data.Repositories;
using QuestionnaireManager.Domain.Model;
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
            return Result.Fail("Questions limit has been reached.");
        }

        var answer = await _answerRepository.GetByIdAsync(command.ParentAnswerId);
        
        if (answer == null)
            return Result.Fail("Answer not found");
        
        if (answer.ChildQuestion != null)
            return Result.Fail("Answer already have a following question");

        var question = new Question(command.Description)
        {
            QuestionnaireId = command.QuestionnaireId
        };

        answer.ChildQuestion = question;
        await _answerRepository.SaveChangesAsync();

        questionnaire.Questions.Add(question);
        await _questionnaireRepository.SaveChangesAsync();
        
        return Result.Ok();
    }
}