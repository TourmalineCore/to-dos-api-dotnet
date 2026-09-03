using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.ToDos;

[ApiController]
[Route("to-dos")]
public class ToDosController : ControllerBase
{
  private static long _nextToDoId = 0;
  private static readonly ConcurrentDictionary<long, ToDo> ToDos = new();

  [HttpPost]
  public Task<CreateToDoResponse> CreateToDoAsync(
    [Required] [FromBody] CreateToDoRequest createToDoRequest
  )
  {
    var newToDo = new ToDo
    {
      Id = Interlocked.Increment(ref _nextToDoId),
      Name = createToDoRequest.Name,
    };

    ToDos[newToDo.Id] = newToDo;

    return Task.FromResult(
      new CreateToDoResponse() { NewToDoId = newToDo.Id }
    );
  }

  [HttpGet]
  public Task<GetToDosResponse> GetToDosAsync()
  {
    return Task.FromResult(
      new GetToDosResponse
      {
        ToDos = ToDos
          .Values.Select(x => new ToDoDto { Id = x.Id, Name = x.Name })
          .ToList(),
      }
    );
  }

  [HttpDelete]
  public Task<DeleteToDoResponse> DeleteToDoAsync(
    [Required] [FromQuery] long toDoId
  )
  {
    return Task.FromResult(
      new DeleteToDoResponse { IsDeleted = ToDos.Remove(toDoId, out _) }
    );
  }
}
