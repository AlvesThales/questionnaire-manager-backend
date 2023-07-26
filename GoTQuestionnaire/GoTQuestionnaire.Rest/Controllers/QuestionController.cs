using GoTQuestionnaire.Domain.Model;
using GoTQuestionnaire.Rest.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoTQuestionnaire.Rest.Controllers;

[Route("[controller]")]
public class QuestionController : BaseController
{
    public QuestionController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    [Route("Questions")]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult Get()
    {
        return Ok(new Question("What is your favorite House?"));
    }
}