using GoTQuestionnaire.Rest.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GoTQuestionnaire.Rest.Controllers;

public class BaseController : ControllerBase
{
    public IMediator Mediator { get; }
    
    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
    
    protected IActionResult Ok<T>(T result)
    {
        // return base.Ok(Envelope.Ok(result));
        return base.Ok();
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