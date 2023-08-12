﻿using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Application.Commands.CreateQuestionnaire;
using QuestionnaireManager.Application.Commands.DeleteQuestionnaire;
using QuestionnaireManager.Application.Commands.UpdateQuestionnaire;
using QuestionnaireManager.Application.Queries.GetQuestionnaireById;
using QuestionnaireManager.Application.Queries.GetQuestionnaires;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Model.Request;
using QuestionnaireManager.Rest.Model.Response;
using QuestionnaireManager.Rest.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace QuestionnaireManager.Rest.Controllers;

[Route("questionnaires")]
public class QuestionnairesController : BaseController
{
    public QuestionnairesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateQuestionnaire([FromBody] CreateQuestionnaireRequest request)
    {

        var command =
            new CreateQuestionnaireCommand(
                new Questionnaire(request.Name, request.MaxAnswers, request.MaxQuestions));

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ? Problem() : Created();
    }
    
    [HttpGet(Name = "GetAllQuestionnaires")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> GetAllQuestionnaires()
    {
        var query = new GetQuestionnairesQuery();
        var questionnaires = await Mediator.DispatchAsync(query);
        
        var response = questionnaires.Select(questionnaire => new GetQuestionnaireResponse
        {
            Id = questionnaire.Id,
            Name = questionnaire.Name,
            MaxAnswers = questionnaire.MaxAnswers,
            MaxQuestions = questionnaire.MaxQuestions,
            HasRoot = questionnaire.HasRoot,
            Questions = questionnaire.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Description = q.Description,
                IsRoot = q.IsRoot,
                Links = new List<LinkDto>
                {
                    new()
                    {
                        Href = Url.Link("GetQuestionById", new {questionnaireId = questionnaire.Id, questionId = q.Id}),
                        Rel = "child",
                        Method = "GET"
                    }
                }
            }).ToList(),
            Links = new List<LinkDto>
            {
                new()
                {
                    Href = Url.Link("GetAllQuestionnaires", new { questionnaireId = questionnaire.Id }),
                    Rel = "self",
                    Method = "GET"
                },
                new()
                {
                    Href = Url.Link("GetQuestionById", new {questionnaireId = questionnaire.Id, questionId = questionnaire.Questions.FirstOrDefault(q => q.IsRoot).Id }),
                    Rel = "rootQuestion",
                    Method = "GET"
                }
            }
        }).ToList();

        return Ok(response);
    }
    
    [HttpGet("{id:int}", Name = "GetQuestionnaireById")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> GetQuestionnaireById(int id)
    {
        var query = new GetQuestionnaireByIdQuery(id);
        var questionnaire = await Mediator.DispatchAsync(query);
        if (questionnaire == null)
            NotFound($"Questionnaire with ID {id} not found.");

        return Ok(new GetQuestionnaireResponse
            {
                Id = questionnaire.Id,
                Name = questionnaire.Name,
                MaxAnswers = questionnaire.MaxAnswers,
                MaxQuestions = questionnaire.MaxQuestions,
                HasRoot = questionnaire.HasRoot,
                Questions = questionnaire.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Description = q.Description,
                    IsRoot = q.IsRoot,
                    Links = new List<LinkDto>
                    {
                        new()
                        {
                            Href = Url.Link("GetQuestionById", new {questionnaireId = questionnaire.Id, questionId = q.Id}),
                            Rel = "child",
                            Method = "GET"
                        }
                    }
                }).ToList(),
                Links = new List<LinkDto>
                {
                    new()
                    {
                        Href = Url.Link("GetQuestionnaireById", new { id = questionnaire.Id }),
                        Rel = "self",
                        Method = "GET"
                    }
                }
            }
        );
    }
    
    [HttpPut("{id:int}")]
    [SwaggerResponse(200, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> UpdateQuestionnaire(int id, [FromBody] UpdateQuestionnaireRequest request)
    {
        var command =
            new UpdateQuestionnaireCommand(
                id, request.Name, request.MaxAnswers, request.MaxQuestions);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();

    }
    
    [HttpDelete("{id:int}")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request", typeof(Envelope))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", typeof(Envelope))]
    public async Task<IActionResult> Delete(int id)
    {
        var command =
            new DeleteQuestionnaireCommand(id);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();;
    }
}