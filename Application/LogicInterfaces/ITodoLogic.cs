using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters);

    Task<SearchTodoParametersDto> GetAsyncSingle(int id);
    Task UpdateAsync(TodoUpdateDto todo);

    Task DeleteAsync(int id);
}