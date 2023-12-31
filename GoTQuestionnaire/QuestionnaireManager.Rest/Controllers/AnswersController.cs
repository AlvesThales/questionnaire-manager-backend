﻿using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Application.Commands.CreateAnswer;
using QuestionnaireManager.Application.Commands.DeleteAnswer;
using QuestionnaireManager.Application.Commands.DeleteQuestion;
using QuestionnaireManager.Application.Commands.UpdateAnswer;
using QuestionnaireManager.Application.Commands.UpdateQuestion;
using QuestionnaireManager.Application.Queries.GetAnswerById;
using QuestionnaireManager.Application.Queries.GetQuestionById;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Model.Request;
using QuestionnaireManager.Rest.Model.Response;
using QuestionnaireManager.Rest.Services;
using QuestionnaireManager.Rest.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace QuestionnaireManager.Rest.Controllers;

[Route("questionnaires/{questionnaireId:int}/questions/{questionId:int}")]
public class AnswersController : BaseController
{
    private readonly IAnswerEnrichmentService _answerEnrichmentService;
    public AnswersController(IMediator mediator, IAnswerEnrichmentService answerEnrichmentService) : base(mediator)
    {
        _answerEnrichmentService = answerEnrichmentService;
    }

    [HttpPost]
    [Route("answers")]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerRequest request, int questionnaireId,
        int questionId)
    {
        var enhancedAnswer = await _answerEnrichmentService.EnrichAnswerAsync(request.Description);
        var command =
            new CreateAnswerCommand(questionnaireId, questionId, enhancedAnswer);
        var result = await Mediator.DispatchAsync(command);
        var createdAnswer = new CreatedAnswerDto(enhancedAnswer);
        if (!result.Failure) return Created("", createdAnswer);

    return result.ErrorMessage switch
        {
            "Questionnaire not found or contains no questions" => NotFound($"Questionnaire with ID {questionnaireId} not found."),
            "Question not found" => NotFound($"Question with ID {questionId} not found."),
            "Answers limit has been reached" => Unprocessable($"Question with ID {questionId} has reached it's answer limit. Can't create more answers."),
            _ => Problem(result.ErrorMessage)
        };
    }

    [HttpGet("answers/{answerId:int}", Name = "GetAnswerById")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> GetAnswerById(int questionnaireId, int questionId, int answerId)
    {
        var query = new GetAnswerByIdQuery(questionnaireId, questionId, answerId);
        var answer = await Mediator.DispatchAsync(query);
        if (answer == null)
            return NotFound($"Answer with ID {answerId} not found.");
        
        return Ok(new GetAnswerResponse
            {
                Id = answer.Id,
                ParentQuestionId = answer.ParentQuestionId,
                Description = answer.Description,
                ChildQuestion = answer.ChildQuestion != null ? new QuestionDto
                {
                    Id = answer.ChildQuestion.Id,
                    Description = answer.ChildQuestion.Description,
                    IsRoot = answer.ChildQuestion.IsRoot,
                    Links = new List<LinkDto>
                    {
                        new()
                        {
                            Href = Url.Link("GetQuestionById", new {questionnaireId = questionnaireId, questionId = answer.ChildQuestion.Id}),
                            Rel = "next-question",
                            Method = "GET"
                        }
                    }
                } : null,
                Links = new List<LinkDto>
                {
                    new()
                    {
                        Href = Url.Link("GetAnswerById", new { questionnaireId, questionId, answerId }),
                        Rel = "self",
                        Method = "GET"
                    },
                    new()
                    {
                        Href = Url.Link("CreateQuestion", new { questionnaireId }) + "/questions",
                        Rel = "create-subsequent-question",
                        Method = "POST"
                    }
                }
            }
        );
    }
    
    [HttpPut("answers/{answerId:int}")]
    [SwaggerResponse(200, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> UpdateAnswer(int answerId, [FromBody] UpdateAnswerRequest request)
    {
        var command =
            new UpdateAnswerCommand(
                answerId, request.Description);
        var result = await Mediator.DispatchAsync(command);
        return result.Failure ?  NotFound() : NoContent();

    }
    
    [HttpDelete("answers/{answerId:int}")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request", typeof(Envelope))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", typeof(Envelope))]
    public async Task<IActionResult> DeleteAnswer(int answerId)
    {
        var command =
            new DeleteAnswerCommand(answerId);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();;
    }
}