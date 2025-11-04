using learning_center_webapi.Contexts.Enrolments.Application.CommandServices;
using learning_center_webapi.Contexts.Enrolments.Application.QueryServices;
using learning_center_webapi.Contexts.Enrolments.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace learning_center_webapi.Contexts.Enrolments.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class EnrolmentController(EnrolmentCommandService commandService, EnrolmentQueryService queryService)
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
        var result = await commandService.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEnrolmentCommand command)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}