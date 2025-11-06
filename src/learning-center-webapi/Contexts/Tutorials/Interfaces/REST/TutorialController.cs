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
    //IStringLocalizer<TutorialController> localizer,
    IStringLocalizerFactory factory
) : ControllerBase
{
    private readonly ITutorialCommandService _tutorialCommandService = tutorialCommandService;
    private readonly ITutorialQueryService _tutorialQueryService = tutorialQueryService;
    //private readonly IStringLocalizer<TutorialController> localizer = localizer;
    

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetByidTutorial(id);
        var tutorial = await _tutorialQueryService.Handle(query);

        if (tutorial == null)
        {
            var notFoundMessage = (string)localizer["TutorialNotFound"];
            return StatusCode(404, new { message = notFoundMessage });
        }

        var resources = TutorialResourceFromEntityAssembler.ToResource(tutorial);
        return Ok(resources);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTutorialCommand command)
    {
        try
        {
            var result = await _tutorialCommandService.Handle(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id },
                new { id = result.Id, message = localizer["TutorialCreatedSuccessfully"] });
        }
        catch (ArgumentException ex)
        {
            return StatusCode(409, new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = localizer["InternalServerError"] });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTutorialCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _tutorialCommandService.Handle(command);
            return Ok(new { message = localizer["TutorialUpdatedSuccessfully"] });
        }
        catch (TutorialNotFoundException)
        {
            return StatusCode(404, new { message = localizer["TutorialNotFound"] });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = localizer["InternalServerError"] });
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateAuthorTutorialCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _tutorialCommandService.Handle(command);
            return Ok(new { message = localizer["AuthorUpdatedSuccessfully"] });
        }
        catch (TutorialNotFoundException)
        {
            return StatusCode(404, new { message = localizer["TutorialNotFound"] });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = localizer["InternalServerError"] });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteTutorialCommand { Id = id };
            var result = await _tutorialCommandService.Handle(command);

            if (result)
            {
                var message = (string)localizer["TutorialDeletedSuccessfully"];
                return Ok(new { message });
            }

            var errorMessage = (string)localizer["CouldNotDeleteTutorial"];
            return StatusCode(400, new { message = errorMessage });
        }
        catch (TutorialNotFoundException)
        {
            var notFoundMessage = (string)localizer["TutorialNotFound"];
            return StatusCode(404, new { message = notFoundMessage });
        }
    }
}
