﻿namespace QuestionnaireManager.Rest.Model.Response;

public class GetQuestionnaireResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MaxAnswers { get; set; }
    public int MaxQuestions { get; set; }
    public virtual IEnumerable<QuestionDto>? Questions { get; set; } = new List<QuestionDto>();
    public List<LinkDto> Links { get; set; }
}