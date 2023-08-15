using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Application.Commands.CreateQuestionnaire;
using QuestionnaireManager.Application.Commands.DeleteQuestionnaire;
using QuestionnaireManager.Application.Commands.UpdateQuestionnaire;
using QuestionnaireManager.Application.Queries.GetQuestionnaireById;
using QuestionnaireManager.Application.Queries.GetQuestionnaires;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Model.Mappers;
using QuestionnaireManager.Rest.Model.Request;
using QuestionnaireManager.Rest.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace QuestionnaireManager.Rest.Controllers;

[Route("questionnaires")]
public class QuestionnairesController : BaseController
{
    private readonly ILogger<QuestionnairesController> _logger;
    public QuestionnairesController(IMediator mediator, ILogger<QuestionnairesController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateQuestionnaire([FromBody] CreateQuestionnaireRequest request)
    {
        _logger.LogInformation("Received request to create questionnaire: {RequestName}", request.Name);
        
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
        _logger.LogInformation("Retrieving all questionnaires");
        var query = new GetQuestionnairesQuery();
        var questionnaires = await Mediator.DispatchAsync(query);

        var response = questionnaires.Select(questionnaire => QuestionnaireMapper.Map(questionnaire, Url));

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
        _logger.LogInformation("Retrieving questionnaire with id: {ID}", id);
        var query = new GetQuestionnaireByIdQuery(id);
        var questionnaire = await Mediator.DispatchAsync(query);
        if (questionnaire == null)
            NotFound($"Questionnaire with ID {id} not found.");

        return Ok(QuestionnaireMapper.Map(questionnaire, Url));
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
                id, request.Name, request.MaxQuestions, request.MaxAnswers);

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
        _logger.LogInformation("Deleting questionnaire with id: {Id}", id);
        
        var command =
            new DeleteQuestionnaireCommand(id);

        var result = await Mediator.DispatchAsync(command);

        return result.Failure ?  NotFound() : NoContent();;
    }
}