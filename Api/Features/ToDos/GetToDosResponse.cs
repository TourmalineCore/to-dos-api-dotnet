namespace Api.Features.ToDos;

public class GetToDosResponse
{
  public required List<ToDoDto> ToDos { get; set; }
}

public class ToDoDto
{
  public required long Id { get; set; }

  public required string Name { get; set; }
}
