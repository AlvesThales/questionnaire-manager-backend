using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Application.Commands.CreateQuestion;
using QuestionnaireManager.Application.Commands.CreateRootQuestion;
using QuestionnaireManager.Application.Commands.DeleteQuestion;
using QuestionnaireManager.Application.Commands.UpdateQuestion;
using QuestionnaireManager.Application.Queries.GetQuestionById;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Model.Request;
using QuestionnaireManager.Rest.Model.Response;
using QuestionnaireManager.Rest.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace QuestionnaireManager.Rest.Controllers;

[Route("questionnaires/{questionnaireId:int}")]
public class QuestionsController : BaseController
{
    public QuestionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("root-question")]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateRootQuestion([FromBody] CreateRootQuestionRequest request, int questionnaireId)
    {
        var command =
            new CreateRootQuestionCommand(questionnaireId, request.Description);

        var result = await Mediator.DispatchAsync(command);

        if (!result.Failure) return Created();
        
        return result.ErrorMessage switch
        {
            "Questionnaire not found" => NotFound($"Questionnaire with ID {questionnaireId} not found."),
            "Questionnaire already has a root question" => Unprocessable($"Questionnaire already has a root question. Delete the current one before trying to add a new one."),
            _ => Problem(result.ErrorMessage)
        };
    }
    
    [HttpPost(Name = "CreateQuestion")]
    [Route("questions")]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request, int questionnaireId)
    {
        var command =
            new CreateQuestionCommand(questionnaireId, request.ParentAnswerId, request.Description);
        var result = await Mediator.DispatchAsync(command);
        
        if (!result.Failure) return Created();
        
        return result.ErrorMessage switch
        {
            "Questionnaire not found" => NotFound($"Questionnaire with ID {questionnaireId} not found."),
            "Answer not found" => NotFound($"Answer with ID {request.ParentAnswerId} not found."),
            "Answer already have a following question" => Unprocessable($"Answer already have a following question. Delete the current one before trying to add a new one."),
            "Questions limit has been reached" => Unprocessable($"Questionnaire with ID {questionnaireId} has reached it's question limit. Can't create more questions."),
            _ => Problem(result.ErrorMessage)
        };
    }
    
    [HttpGet("questions/{questionId:int}", Name = "GetQuestionById")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> GetQuestionById(int questionnaireId, int questionId)
    {
        var query = new GetQuestionByIdQuery(questionnaireId, questionId);
        var question = await Mediator.DispatchAsync(query);
        if (question == null) 
            return NotFound($"Question with ID {questionId} not found.");
            
        return Ok(new GetQuestionResponse
            {
                Id = question.Id,
                QuestionnaireId = question.QuestionnaireId,
                Description = question.Description,
                IsRoot = question.IsRoot,
                Answers = question.Answers.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Description = a.Description,
                    Links = new List<LinkDto>
                    {
                        new()
                        {
                            Href = Url.Link("GetAnswerById", new {questionnaireId = questionnaireId, questionId = questionId, answerId = a.Id}),
                            Rel = "child",
                            Method = "GET"
                        }
                    }
                }).ToList(),
                Links = new List<LinkDto>
                {
                    new()
                    {
                        Href = Url.Action("GetQuestionById"),
                        Rel = "self",
                        Method = "GET"
                    }
                }
            }
        );
    }
    
    [HttpPut("questions/{questionId:int}")]
    [SwaggerResponse(200, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] UpdateQuestionRequest request)
    {
        var command =
            new UpdateQuestionCommand(
                questionId, request.Description);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();

    }
    
    [HttpDelete("questions/{questionId:int}")]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request", typeof(Envelope))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(500, "Internal Server Error", typeof(Envelope))]
    public async Task<IActionResult> DeleteQuestion(int questionId)
    {
        var command =
            new DeleteQuestionCommand(questionId);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();;
    }
}