namespace GoTQuestionnaire.Domain.Model;

public class Question
{
    public string Description { get; set; }

    public Question(string description)
    {
        Description = description;
    }

    public override string ToString()
    {
        return Description;
    }
}