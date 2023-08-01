namespace GoTQuestionnaire.Domain.Model;

public class Answer
{
    public string Description { get; set; }

    public Answer(string description)
    {
        Description = description;
    }
}