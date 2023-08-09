using Microsoft.AspNetCore.Mvc;
using QuestionnaireManager.Rest.Controllers.Utils;
using QuestionnaireManager.Rest.Utils;

namespace QuestionnaireManager.Rest.Controllers;

public class BaseController : ControllerBase
{
    public IMediator Mediator { get; }
    
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
    
    protected IActionResult Ok<T>(T result)
    {
        return base.Ok(Envelope.Ok(result));
    }
    
    protected IActionResult Created()
    {
        return StatusCode(201);
    }

    protected IActionResult Unprocessable(string message)
    {
        return base.UnprocessableEntity(message);
    }
}