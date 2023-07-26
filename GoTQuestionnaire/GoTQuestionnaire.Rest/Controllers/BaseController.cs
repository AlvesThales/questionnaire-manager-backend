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
    
    protected IActionResult Created()
    {
        return StatusCode(201);
    }
}