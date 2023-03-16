namespace Shared.DTOs;

public class SearchSingleTodoParameterDto
{
    public int id { get; }

    public SearchSingleTodoParameterDto(int id)
    {
        this.id = id;
    }
}