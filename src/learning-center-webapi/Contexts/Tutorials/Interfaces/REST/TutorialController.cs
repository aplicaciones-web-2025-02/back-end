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

    
     private readonly IStringLocalizer localizer = factory.Create("Tutorials.TutorialController", "learning_center_webapi");
    
    
    private readonly ITutorialCommandService _tutorialCommandService = tutorialCommandService;
    private readonly ITutorialQueryService _tutorialQueryService = tutorialQueryService;

    // Classic constructor (commented for reference)
    /*
    public TutorialController(ITutorialQueryService tutorialQueryService,
        ITutorialCommandService tutorialCommandService)
    {
        _tutorialQueryService = tutorialQueryService;
        _tutorialCommandService = tutorialCommandService;
    }
    */

    /// // GET: api/
    /// <TutorialController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllTutorials();
        var tutorials = await _tutorialQueryService.Handle(query);

        if (!tutorials.Any()) return NotFound( new { message = localizer["TutorialNotFound"]});

        var resources = tutorials.Select(TutorialResourceFromEntityAssembler.ToResource).ToList();

        return Ok(resources);
    }

    // GET api/<TutorialController>/5
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


    // POST api/<TutorialController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTutorialCommand command)
    {
        var resul = await _tutorialCommandService.Handle(command);
        return CreatedAtAction(nameof(Get), new { id = resul.Id }, resul.Id);
    }

    // PUT api/<TutorialController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTutorialCommand command)
    {
        command.Id = id;
        var result = await _tutorialCommandService.Handle(command);
        return Ok("Tutorial updated successfully.");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateAuthorTutorialCommand command)
    {
        command.Id = id;
        var result = await _tutorialCommandService.Handle(command);
        return Ok("Author updated successfully.");
    }

    // DELETE api/<TutorialController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTutorialCommand { Id = id };
        var result = await _tutorialCommandService.Handle(command);

        if (result)
        {
            var successMessage = string.Format(localizer["TutorialDeletedWithId"], id);
            return Ok(new { message = successMessage });
        }

        var failureMessage = string.Format(localizer["TutorialDeleteFailedWithId"], id);
        return StatusCode(407, new { message = failureMessage });
    }

}