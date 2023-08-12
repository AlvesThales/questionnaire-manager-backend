namespace QuestionnaireManager.Rest.Model.Response;

public class GetQuestionResponse
{
    public int Id { get; set; }
    public int QuestionnaireId { get; set; }
    public string Description { get; set; }
    public List<AnswerDto> Answers { get; set; }
    public bool IsRoot { get; set; }
    public List<LinkDto> Links { get; set; }
}