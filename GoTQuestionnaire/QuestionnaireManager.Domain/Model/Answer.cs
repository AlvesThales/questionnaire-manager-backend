namespace QuestionnaireManager.Domain.Model;

public class Answer
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int ParentQuestionId { get; set; }
    public virtual Question? ChildQuestion { get; set; }

    public Answer(string description)
    {
        Description = description;
    }
}