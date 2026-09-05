using System.ComponentModel.DataAnnotations;
using Application.Features.ToDos.Handlers.CreateToDo;
using Application.Features.ToDos.Handlers.DeleteToDo;
using Application.Features.ToDos.Handlers.GetToDos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.ToDos;

[ApiController]
[Route("to-dos")]
public class ToDosController : ControllerBase
{
  [HttpPost]
  public Task<CreateToDoResponse> CreateToDoAsync(
    [Required] [FromBody] CreateToDoRequest createToDoRequest,
    [FromServices] CreateToDoHandler createToDoHandler
  )
  {
    return createToDoHandler.HandleAsync(createToDoRequest);
  }

  [HttpGet]
  public Task<GetToDosResponse> GetToDosAsync(
    [FromServices] GetToDosHandler getToDosHandler
  )
  {
    return getToDosHandler.HandleAsync();
  }

  [HttpDelete]
  public Task<DeleteToDoResponse> DeleteToDoAsync(
    [Required] [FromQuery] long toDoId,
    [FromServices] DeleteToDoHandler deleteToDoHandler
  )
  {
    return deleteToDoHandler.HandleAsync(toDoId);
  }
}
