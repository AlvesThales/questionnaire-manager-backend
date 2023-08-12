namespace QuestionnaireManager.Domain.Model;

public class Question
{
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public int? ParentAnswerId { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Answer>? Answers { get; set; }
    public bool IsRoot { get; set; } = false;
    
    public Question(string description)
    {
        Description = description;
    }

    public override string ToString()
    {
        return Description;
    }
}