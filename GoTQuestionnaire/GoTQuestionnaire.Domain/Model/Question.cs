using GoTQuestionnaire.Infrastructure.Exceptions;

namespace GoTQuestionnaire.Domain.Model;

public class Question
{
    private const int MaxAnswers = 4;
    private const int MaxQuestions = 5;
    private static int _questionsCount = 0;

    private int Id { get; set; }
    private string Description { get; set; }
    private bool IsRoot { get; set; }
    public virtual ICollection<Answer> Answers { get; set; }
    public virtual Answer ParentAnswer { get; set; }
    
    public Question(string description)
    {
        if (_questionsCount.Equals(MaxQuestions))
        {
            throw new QuestionsLimitReachedException();
        }
        Description = description;
        _questionsCount++;
    }

    public void AddAnswer(Answer answer)
    {
        if (Answers.Count.Equals(MaxAnswers))
        {
            throw new AnswersLimitReachedException();
        }
        Answers.Add(answer);
    }
    
    public override string ToString()
    {
        return Description;
    }
}