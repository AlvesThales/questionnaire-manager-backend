namespace QuestionnaireManager.Domain.Model;

public class Questionnaire
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxAnswers { get; set; } = 4;
    public int MaxQuestions { get; set; } = 5;
    public bool HasRoot { get; set; } = false;
    public List<Question> Questions { get; } = new();
}