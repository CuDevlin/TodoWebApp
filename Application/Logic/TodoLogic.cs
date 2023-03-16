using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao todoDao;
    private readonly IUserDao userDao;

    public TodoLogic(ITodoDao todoDao, IUserDao userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }
    
    public async Task<Todo> CreateAsync(TodoCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found");
        }
        
        Todo todo = new Todo(user, dto.Title);

        ValidateTodo(todo);
        Todo created = await todoDao.CreateAsync(todo);
        return created;
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        return todoDao.GetAsync(searchParameters);
    }

    public async Task<SearchTodoParametersDto> GetAsyncSingle(int id)
    {
        Todo? user = await todoDao.GetByIdAsync(id);
        if (user == null)
        {
            throw new Exception($"User with id {userDao.GetByIdAsync(id)} does not exist");
        }

        return new SearchTodoParametersDto(user.Owner.UserName, user.Id, user.IsCompleted, user.Title);
    }

    public async Task UpdateAsync(TodoUpdateDto todo)
    {
        Todo? existing = await todoDao.GetByIdAsync(todo.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {todo.Id} not found!");
        }

        User? user = null;
        if (todo.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)todo.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with ID {todo.OwnerId} was not found");
            }
        }

        if (todo.IsCompleted != null && existing.IsCompleted && (bool)todo.IsCompleted)
        {
            throw new Exception("Cannot un-complete a completed todo");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = todo.Title ?? existing.Title;
        bool completedToUse = todo.IsCompleted ?? existing.IsCompleted;

        Todo updated = new(userToUse, titleToUse)
        {
            IsCompleted = completedToUse,
            Id = existing.Id,
        };
        
        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Todo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("Cannot delete un-completed Todo!");
        }

        await todoDao.DeleteAsync(id);
    }

    private void ValidateTodo(Todo dto)
    {
        if(string.IsNullOrEmpty(dto.Title)) throw new Exception("title cannot be empty");
    }
}