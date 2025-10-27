using Microsoft.AspNetCore.Mvc;

namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST;

[Route("api/[controller]")]
[ApiController]
public class TutorialController : ControllerBase
{
    // GET: api/<TutorialController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new[] { "value1", "value2" };
    }

    // GET api/<TutorialController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<TutorialController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<TutorialController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<TutorialController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}