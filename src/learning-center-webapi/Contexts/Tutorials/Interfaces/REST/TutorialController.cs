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

        var message = new { message = localizer["TutorialNotFound"].Value };
        if (tutorial == null) return NotFound(message );

        var resources = TutorialResourceFromEntityAssembler.ToResource(tutorial);

        return Ok(resources);
    }

    // POST api/<TutorialController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTutorialCommand command)
    {
        try
        {
            var resul = await _tutorialCommandService.Handle(command);

            return CreatedAtAction(nameof(Get), new { id = resul.Id }, resul.Id);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(409, ex.Message);
        }
        catch (Exception ex)
        {
            //logear
            return StatusCode(500, ex.Message);
        }
    }

    // PUT api/<TutorialController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTutorialCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _tutorialCommandService.Handle(command);

            return Ok("Tutorial updated successfully.");
        }
        catch (TutorialNotFoundException exception)
        {
            return StatusCode(404, exception.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateAuthorTutorialCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _tutorialCommandService.Handle(command);
            return Ok("Author updated successfully.");
        }
        catch (TutorialNotFoundException exception)
        {
            return StatusCode(404, exception.Message);
        }
        catch (Exception ex)
        {
            //logear 
            return StatusCode(500, ex.Message);
        }
    }

    // DELETE api/<TutorialController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteTutorialCommand { Id = id };

            var result = await _tutorialCommandService.Handle(command);

            if (result)
                return Ok("Tutorial deleted successfully.");

            return StatusCode(407, "Could not delete the tutorial.");
        }
        catch (TutorialNotFoundException exception)
        {
            return StatusCode(404, exception.Message);
        }
    }
}