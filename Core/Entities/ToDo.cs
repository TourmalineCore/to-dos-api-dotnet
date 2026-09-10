namespace Core.Entities;

public class ToDo
{
  public ToDo() { }

  public long Id { get; set; }

  public required string Name { get; set; }

  public DateTime CreatedAtUtc { get; set; }

  public ToDoStatus GetStatus(IDateTimeProvider dateTimeProvider)
  {
    const int newForDays = 7;
    const int forgottenAfterDays = 28;

    var ageInDays = (dateTimeProvider.UtcNow - CreatedAtUtc).TotalDays;

    if (ageInDays < newForDays)
      return ToDoStatus.New;

    if (ageInDays < forgottenAfterDays)
      return ToDoStatus.Old;

    return ToDoStatus.Forgotten;
  }
}
