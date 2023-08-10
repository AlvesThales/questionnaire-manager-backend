namespace QuestionnaireManager.Rest.Model.Request;

public class UpdateQuestionnaireRequest
{
    public string Name { get; set; }
    public int MaxQuestions { get; set; }
    public int MaxAnswers { get; set; }
}