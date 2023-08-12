namespace QuestionnaireManager.Rest.Model.Response;

public class GetAnswerResponse
{
    public int Id { get; set; }
    public int ParentQuestionId { get; set; }
    public string Description { get; set; }
    public QuestionDto? ChildQuestion { get; set; }
    public List<LinkDto> Links { get; set; }
}