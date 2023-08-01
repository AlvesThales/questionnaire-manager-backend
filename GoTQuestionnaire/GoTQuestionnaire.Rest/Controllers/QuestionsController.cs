﻿using GoTQuestionnaire.Application.Commands.CreateQuestion;
using GoTQuestionnaire.Domain.Model;
using GoTQuestionnaire.Infrastructure.Exceptions;
using GoTQuestionnaire.Rest.Controllers.Utils;
using GoTQuestionnaire.Rest.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoTQuestionnaire.Rest.Controllers;

[Route("[controller]")]
public class QuestionsController : BaseController
{
    public QuestionsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    [SwaggerResponse(200, "Ok")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public IActionResult Get()
    {
        return Ok(new Question("What is your favorite House?"));
    }
    
    [HttpPost]
    [SwaggerResponse(201, "Created")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(422, "UnprocessableEntity")]
    [SwaggerResponse(500, "Internal Server Error")]
    public async Task<IActionResult> Post([FromBody]CreateQuestionRequest request)
    {
        var command =
            new CreateQuestionCommand(
                new Question(request.Description));
        
        var result = await Mediator.DispatchAsync(command);
        
        return result.Failure ? Unprocessable(result.Error) : Created();

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