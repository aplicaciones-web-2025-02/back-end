
using learning_center_webapi.Contexts.Security.Domain.Commands;
using learning_center_webapi.Contexts.Security.Application.CommandServices;
using learning_center_webapi.Contexts.Security.Application.QueryServices;
using learning_center_webapi.Contexts.Security.Interfaces.REST.Resources;
using learning_center_webapi.Contexts.Security.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace learning_center_webapi.Contexts.Security.Interfaces.REST;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserCommandService userCommandService, IUserQueryService userQueryService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await userQueryService.ListAsync();
        var resources = users.Select(UserResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userQueryService.FindByIdAsync(id);
        if (user == null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResource(user);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var user = await userCommandService.Handle(command);
        var resource = UserResourceFromEntityAssembler.ToResource(user);
        return CreatedAtAction(nameof(Create), new { id = resource.Id }, resource.Id);
    }
}