using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/todos")]
[Produces("application/json")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    /// <summary>Returns all todos.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Todo>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<Todo>> GetAll()
    {
        return Ok(_todoService.GetAll());
    }

    /// <summary>Returns a single todo by id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    /// <summary>Creates a new todo.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Todo> Create([FromBody] CreateTodoRequest request)
    {
        var todo = _todoService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    /// <summary>Toggles the completed state of a todo.</summary>
    [HttpPut("{id:int}/toggle")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Todo> Toggle(int id)
    {
        var todo = _todoService.ToggleComplete(id);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    /// <summary>Deletes a todo.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        if (!_todoService.Delete(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
