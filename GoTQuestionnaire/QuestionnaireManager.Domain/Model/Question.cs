using QuestionnaireManager.Infrastructure.Exceptions;

namespace QuestionnaireManager.Domain.Model;

public class Question
{
    public int Id { get; }
    private string Description { get; set; }
    public virtual List<Answer>? Answers { get; set; } = new();
    public virtual Answer? ParentAnswer { get; set; }

    public Question(string description)
    {
        Description = description;
    }

    // public void AddAnswer(Answer answer)
    // {
    //     if (Answers.Count.Equals(MaxAnswers))
    //     {
    //         throw new AnswersLimitReachedException();
    //     }
    //     Answers.Add(answer);
    // }
    
    public override string ToString()
    {
        return Description;
    }
}