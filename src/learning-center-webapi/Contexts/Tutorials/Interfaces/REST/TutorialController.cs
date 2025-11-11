using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
public class TutorialController(
    ITutorialQueryService tutorialQueryService,
    ITutorialCommandService tutorialCommandService,
    IStringLocalizerFactory factory
) : ControllerBase
{
    private readonly ITutorialCommandService _tutorialCommandService = tutorialCommandService;
    private readonly ITutorialQueryService _tutorialQueryService = tutorialQueryService;
    private readonly IStringLocalizer localizer = factory.Create("Tutorials.TutorialController", "learning_center_webapi");

    /// <summary>
    /// Gets all tutorials
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllTutorials();
        var tutorials = await _tutorialQueryService.Handle(query);

        if (!tutorials.Any())
            return NotFound(new { message = localizer["NoTutorialsFound"] });

        var resources = tutorials.Select(TutorialResourceFromEntityAssembler.ToResource).ToList();
        return Ok(resources);
    }
    
    
    /// <summary>
    /// Get active Tutorial by id.
    /// </summary>
    /// <param name="id"> This is the identifier</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetByidTutorial(id);
        var tutorial = await _tutorialQueryService.Handle(query);

        if (tutorial is null)
        {
            var notFoundMessage = localizer["TutorialNotFound"].Value;
            return NotFound(new { message = notFoundMessage });
        }

        var resource = TutorialResourceFromEntityAssembler.ToResource(tutorial);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTutorialCommand command)
    {
        var result = await _tutorialCommandService.Handle(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id },
            new { id = result.Id, message = localizer["TutorialCreated"] });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTutorialCommand command)
    {
        command.Id = id;
        var result = await _tutorialCommandService.Handle(command);
        return Ok(new { message = localizer["TutorialUpdated"] });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateAuthorTutorialCommand command)
    {
        command.Id = id;
        var result = await _tutorialCommandService.Handle(command);
        return Ok(new { message = localizer["TutorialUpdated"] });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTutorialCommand { Id = id };
        var result = await _tutorialCommandService.Handle(command);

        if (result)
        {
            var message = localizer["TutorialDeleted"].Value;
            return Ok(new { message });
        }

        var errorMessage = localizer["ErrorDeletingTutorial"].Value;
        return StatusCode(400, new { message = errorMessage });
    }
}
