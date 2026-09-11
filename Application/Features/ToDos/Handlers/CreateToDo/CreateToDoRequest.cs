using System.ComponentModel.DataAnnotations;

namespace Application.Features.ToDos.Handlers.CreateToDo;

public class CreateToDoRequest
{
  [Required]
  [MaxLength(256)]
  public required string Name { get; set; }
}
