namespace QuestionnaireManager.Rest.Model;

public class CreateQuestionnaireRequest
{
    public string Name { get; set; }
    public int MaxQuestions { get; set; }
    public int MaxAnswers { get; set; }
}