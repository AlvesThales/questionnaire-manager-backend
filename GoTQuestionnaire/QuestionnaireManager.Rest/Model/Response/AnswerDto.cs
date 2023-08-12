namespace QuestionnaireManager.Rest.Model.Response;

public class AnswerDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<LinkDto> Links { get; set; }
}