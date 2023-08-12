namespace QuestionnaireManager.Rest.Model.Response;

public class GetQuestionnaireResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxAnswers { get; set; }
    public int MaxQuestions { get; set; }
    public bool HasRoot { get; set; }
    public virtual ICollection<QuestionDto>? Questions { get; set; }
    public List<LinkDto> Links { get; set; }
}