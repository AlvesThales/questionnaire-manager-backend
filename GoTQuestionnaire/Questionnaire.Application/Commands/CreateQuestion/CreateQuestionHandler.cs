using Questionnaire.Data.Repositories;
using Questionnaire.Domain.Model;
using Questionnaire.Infrastructure.Utils;

namespace Questionnaire.Application.Commands.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<CreateQuestionCommand>
{
    private readonly IQuestionRepository _questionRepository;
    public async Task<Result> HandleAsync(CreateQuestionCommand command)
    {
        return await _questionRepository.
    }
    public void AddQuestion(Question question)
    {
        if (Questions.Count.Equals(MaxQuestions))
        {
            throw new QuestionsLimitReachedException();
        }

        if (!HasRoot)
        {
            RootQuestion = question;
            HasRoot = true;
        }
        
        Questions.Add(question);
    }
}