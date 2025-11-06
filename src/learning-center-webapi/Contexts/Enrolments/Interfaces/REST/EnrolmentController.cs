using learning_center_webapi.Contexts.Enrolments.Application.CommandServices;
using learning_center_webapi.Contexts.Enrolments.Application.QueryServices;
using learning_center_webapi.Contexts.Enrolments.Domain.Model;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace learning_center_webapi.Contexts.Enrolments.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class EnrolmentController(IEnrolmentCommandService commandService, IEnrolmentQueryService queryService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrolments = await queryService.GetAllAsync();
        return Ok(enrolments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var enrolment = await queryService.GetByIdAsync(id);
        if (enrolment == null) return NotFound();
        return Ok(enrolment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnrolmentCommand command)
    {
        try
        {
            var result = await commandService.CreateAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Id);
        }
        catch (TutorialNotExistExceptions ex)
        {
            return StatusCode(407, ex.Message);
        }
        catch (UserNotExistExceptions ex)
        {
            return StatusCode(407, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateEnrolmentCommand command)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
}