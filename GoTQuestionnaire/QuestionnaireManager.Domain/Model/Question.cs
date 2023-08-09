using QuestionnaireManager.Infrastructure.Exceptions;

namespace QuestionnaireManager.Domain.Model;

public class Question
{
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public int ParentAnswerId { get; set; }
    public string Description { get; set; }
    public virtual List<Answer>? Answers { get; set; } = new();
    public virtual Answer? ParentAnswer { get; set; }
    public bool IsRoot { get; set; } = false;
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