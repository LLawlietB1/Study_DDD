﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        var useCase = new RegisterEventUseCase();
        
        var response = useCase.Execute(request);
         
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetByID([FromRoute] Guid id)
    {
        try
        {
            var useCase = new GetEventByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }

        catch (PassInException ex)
        {
            return NotFound(new ResponseErrorJson(ex.Message));
        }

        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Unknown error"));
        }
    }

    
}

