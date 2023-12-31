﻿namespace QuestionnaireManager.Domain.Model;

public class Questionnaire
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxAnswers { get; set; }
    public int MaxQuestions { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }

    public Questionnaire(string name, int maxAnswers, int maxQuestions)
    {
        Name = name;
        MaxAnswers = maxAnswers;
        MaxQuestions = maxQuestions;
    }
}