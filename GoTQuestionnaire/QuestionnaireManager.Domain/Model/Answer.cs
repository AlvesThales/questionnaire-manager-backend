namespace QuestionnaireManager.Domain.Model;

public class Answer
{
    public int Id { get; }
    public string Description { get; set; }
    public virtual Question ParentQuestion { get; set; }
    public virtual Question? ChildQuestion { get; set; }

    public Answer(string description)
    {
        Description = description;
    }
}