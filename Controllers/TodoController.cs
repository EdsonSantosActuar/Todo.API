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
            return NotFound();

        return Ok(ret);
    }

    [HttpGet]
    [Route("/{id:int}")]
    public IActionResult Get(int id, [FromServices] Context context)
    {
        var ret = context.Todos.FirstOrDefault(x => x.Id == id);

        if (ret is null)
            return NotFound();

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

    [HttpPut]
    [Route("/{id:int}")]
    public IActionResult Put(int id, [FromBody] TodoModel todo, [FromServices] Context context)
    {
        var model = context.Todos.FirstOrDefault(x => x.Id == id);

        if (model is null)
            return NotFound();

        model.Title = todo.Title;
        model.Done = todo.Done;

        context.Todos.Update(model);
        var persist = context.SaveChanges();

        if (persist == 0)
            return BadRequest();

        return Ok(model);
    }

    [HttpDelete]
    [Route("/{id:int}")]
    public IActionResult Delete(int id, [FromServices] Context context)
    {
        var model = context.Todos.FirstOrDefault(x => x.Id == id);

        if (model is null)
            return NotFound();

        context.Todos.Remove(model);
        var persist = context.SaveChanges();

        if (persist == 0)
            return BadRequest();

        return Ok();
    }
}