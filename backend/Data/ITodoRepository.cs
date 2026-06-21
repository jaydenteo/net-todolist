using backend.Models;

namespace backend.Data;

public interface ITodoRepository
{
    IReadOnlyList<Todo> GetAll();
    Todo? GetById(int id);
    Todo Add(Todo todo);
    Todo? Update(Todo todo);
    bool Delete(int id);
}
