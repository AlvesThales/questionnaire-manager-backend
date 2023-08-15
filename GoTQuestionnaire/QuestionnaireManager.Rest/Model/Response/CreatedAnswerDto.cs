namespace QuestionnaireManager.Rest.Model.Response;

public class CreatedAnswerDto
{
    public CreatedAnswerDto(string answer)
    {
        Answer = answer;
    }

    public string Answer { get; set; }
}