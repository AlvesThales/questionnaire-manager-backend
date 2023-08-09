using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Application.Commands.CreateQuestionnaire;
using QuestionnaireManager.Application.Queries.GetQuestionnaireById;
using QuestionnaireManager.Domain.Model;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Model;
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
    
    [HttpGet]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetQuestionnaireByIdQuery(id);
        var questionnaire = await Mediator.DispatchAsync<Questionnaire>(query);
        return questionnaire == null ? 
            NotFound($"Questionnaire with ID {id} not found.") : 
            Ok(questionnaire);
    }
    
    [HttpPost]
    [Route("questions")]

    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> CreateQuestion([FromBody]CreateQuestionRequest request)
    {
        throw new NotImplementedException();
        // var command =
        //     new CreateQuestionCommand(
        //         new Question(request.Description));
        //
        // var result = await Mediator.DispatchAsync(command);
        //
        // return result.Failure ? Unprocessable(result.ErrorMessage) : Created();

        // try
        // {
        //     var question = new Question(request.Description);
        //     return Created();
        // }
        // catch (QuestionsLimitReached e)
        // {
        //     return Unprocessable(e.Message);
        // }
    }
}