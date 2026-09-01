using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.ToDos;

[ApiController]
[Route("to-dos")]
public class ToDosController : ControllerBase
{
  [HttpPost]
  public Task<CreateToDoResponse> CreateToDoAsync([Required] [FromBody] CreateToDoRequest createToDoRequest)
  {
    throw new NotImplementedException();
  }

  [HttpGet]
  public Task<GetToDosResponse> GetToDosAsync()
  {
    throw new NotImplementedException();
  }

  [HttpDelete]
  public Task<DeleteToDoResponse> DeleteToDoAsync([Required] [FromQuery] long toDoId)
  {
    throw new NotImplementedException();
  }
}
