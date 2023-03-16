using Shared.Models;

namespace Shared.DTOs;

public interface IUserDao
{
    public Task<User> CreateAsync(User user);

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    public Task<User> GetByUsernameAsync(string userName);

    public Task<User> GetByIdAsync(int id);
}