using Shared.Models;

namespace Shared.DTOs;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto userToCreate);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);

    public Task<User> GetAsyncSingle(SearchSingleTodoParameterDto dto);
}