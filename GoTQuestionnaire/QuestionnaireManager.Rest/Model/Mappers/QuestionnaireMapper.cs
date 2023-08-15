using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Rest.Model.Response;

namespace QuestionnaireManager.Rest.Model.Mappers;

public static class QuestionnaireMapper
{
    public static GetQuestionnaireResponse Map(Questionnaire questionnaire, IUrlHelper url)
    {
        return new GetQuestionnaireResponse
        {
            Id = questionnaire.Id,
            Name = questionnaire.Name,
            MaxAnswers = questionnaire.MaxAnswers,
            MaxQuestions = questionnaire.MaxQuestions,
            Questions = questionnaire.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Description = q.Description,
                IsRoot = q.IsRoot,
                Links = new List<LinkDto>
                {
                    new()
                    {
                        Href = url.Link("GetQuestionById",
                            new { questionnaireId = questionnaire.Id, questionId = q.Id }),
                        Rel = "child",
                        Method = "GET"
                    }
                }
            }).ToList(),
            Links = new List<LinkDto>
            {
                new()
                {
                    Href = url.Link("GetQuestionnaireById", new { id = questionnaire.Id }),
                    Rel = "self",
                    Method = "GET"
                }
            }
        };
    }
}
