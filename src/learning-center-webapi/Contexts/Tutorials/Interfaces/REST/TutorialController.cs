using learning_center_webapi.Contexts.Tutorials.Domain.Attributes;
using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST;

[Route("api/v1/[controller]")]
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
    /// <returns>all active tutorials </returns>
    [HttpGet]
    [CustomAuthorizeAtrribute("mkt,admin")]
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
    /// <param name="id"><see cref="localizer["TutorialIdDescription"]"/></param>
    /// <returns>A found active tutorial by id</returns>
    [HttpGet("{id}")]
    [CustomAuthorizeAtrribute("mkt,admin")]
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

    /// <summary>
    /// Creat a new tutorial
    /// </summary>
    /// <remarks>
    /// - this method is only for admin users.
    /// - this endopont is available at night time.
    /// 
    /// POST /Tutorial
    /// { 
    ///         "title": "Tutorial Java",
    ///          "description": "This is tutorial for learning Java from scratch",   
    ///          "publishedDate": "2025-11-11T01:15:13.623Z",
    ///          "author": "string",
    ///          "authorEmail": "string",
    ///          "level": 0,
    ///          "views": 0,
    ///           "tags": "open source"
    ///}
    /// </remarks>
    /// <returns>The recently created id of tutorial</returns>
    /// <response code="201">Tutorial created successfully</response>
    /// <response code="409">Error with bussiness rules</response>
    /// <response code="500">Internal server error for unhandled expections</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [CustomAuthorizeAtrribute("admin")]
    
    public async Task<IActionResult> Post([FromBody] CreateTutorialCommand command)
    {
        var result = await _tutorialCommandService.Handle(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id },//201
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
        return Ok(new { message = localizer["TutorialUpdated"] });//200
    }

    [HttpDelete("{id}")]
    [Route("my-ow-route")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTutorialCommand { Id = id };
        var result = await _tutorialCommandService.Handle(command);

        if (result)
        {
            var message = localizer["TutorialDeleted"].Value;
            //return Ok(new { message });
            return StatusCode(508);

        }

        var errorMessage = localizer["ErrorDeletingTutorial"].Value;
        return StatusCode(400, new { message = errorMessage });//bad
    }
}
