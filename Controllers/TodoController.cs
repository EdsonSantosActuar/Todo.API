using Microsoft.AspNetCore.Mvc;
using Todo.API.Data;
using Todo.API.Models;

namespace Todo.API.Controllers;

[ApiController]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult Get([FromServices] Context context)
    {
        var ret = context.Todos.ToList();

        if (ret.Count <= 0)
            return NoContent();

        return Ok(ret);
    }

    [HttpPost]
    [Route("/")]
    public IActionResult Post([FromBody] TodoModel todo, [FromServices] Context context)
    {
        context.Add(todo);
        var persist = context.SaveChanges();

        if (persist == 0)
            return BadRequest();

        return Ok(todo);
    }
}