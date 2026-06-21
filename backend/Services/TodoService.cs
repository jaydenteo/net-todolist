using backend.Data;
using backend.Models;

namespace backend.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<Todo> GetAll() => _repository.GetAll();

    public Todo? GetById(int id) => _repository.GetById(id);

    public Todo Create(CreateTodoRequest request)
    {
        var todo = new Todo
        {
            Title = request.Title.Trim(),
            IsCompleted = false
        };

        return _repository.Add(todo);
    }

    public Todo? ToggleComplete(int id)
    {
        var todo = _repository.GetById(id);
        if (todo is null)
        {
            return null;
        }

        todo.IsCompleted = !todo.IsCompleted;
        return _repository.Update(todo);
    }

    public bool Delete(int id) => _repository.Delete(id);
}
