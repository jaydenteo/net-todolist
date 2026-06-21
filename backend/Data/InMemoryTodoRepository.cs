using backend.Models;

namespace backend.Data;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly List<Todo> _todos = [];
    private int _nextId = 1;
    private readonly object _lock = new();

    public IReadOnlyList<Todo> GetAll()
    {
        lock (_lock)
        {
            return _todos.ToList();
        }
    }

    public Todo? GetById(int id)
    {
        lock (_lock)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }
    }

    public Todo Add(Todo todo)
    {
        lock (_lock)
        {
            todo.Id = _nextId++;
            todo.CreatedAt = DateTimeOffset.UtcNow;
            _todos.Add(todo);
            return todo;
        }
    }

    public Todo? Update(Todo todo)
    {
        lock (_lock)
        {
            var index = _todos.FindIndex(t => t.Id == todo.Id);
            if (index < 0)
            {
                return null;
            }

            _todos[index] = todo;
            return todo;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo is null)
            {
                return false;
            }

            _todos.Remove(todo);
            return true;
        }
    }
}
