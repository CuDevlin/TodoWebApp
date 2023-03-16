using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);
    Task<Todo> GetByIdAsync(int id);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(int id);
}