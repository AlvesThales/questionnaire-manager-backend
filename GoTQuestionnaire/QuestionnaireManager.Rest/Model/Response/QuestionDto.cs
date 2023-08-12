namespace QuestionnaireManager.Rest.Model.Response;

public class QuestionDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsRoot { get; set; }
    public List<LinkDto> Links { get; set; }
}