using backend.Models;

namespace backend.Services;

public interface ITodoService
{
    IReadOnlyList<Todo> GetAll();
    Todo? GetById(int id);
    Todo Create(CreateTodoRequest request);
    Todo? ToggleComplete(int id);
    bool Delete(int id);
}
