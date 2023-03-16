using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if(existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public static void ValidateData(UserCreationDto utc)
    {
        string userName = utc.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters");
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public Task<User> GetAsyncSingle(SearchSingleTodoParameterDto dto)
    {
        return userDao.GetByIdAsync(dto.id);
    }
}