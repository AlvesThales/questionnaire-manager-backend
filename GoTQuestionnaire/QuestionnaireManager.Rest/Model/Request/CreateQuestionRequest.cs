﻿namespace QuestionnaireManager.Rest.Model.Request;

public class CreateQuestionRequest
{
    public string Description { get; set; }
    public int ParentAnswerId { get; set; }
}