namespace Core.Entities;

public class ToDo
{
  public ToDo() { }

  public long Id { get; set; }

  public required string Name { get; set; }

  public DateTime CreatedAtUtc { get; set; }

  public ToDoStatus GetStatus(IDateTimeProvider dateTimeProvider)
  {
    var utcNow = dateTimeProvider.UtcNow;

    if (CreatedAtUtc > utcNow.AddDays(-7) && CreatedAtUtc < utcNow)
    {
      return ToDoStatus.New;
    }
    else if (
      CreatedAtUtc > utcNow.AddDays(-28)
      && CreatedAtUtc <= utcNow.AddDays(-7)
    )
    {
      return ToDoStatus.Old;
    }
    else if (CreatedAtUtc <= utcNow.AddDays(-28))
    {
      return ToDoStatus.Forgotten;
    }

    throw new ArgumentOutOfRangeException(
      $"Not expected path of ${nameof(GetStatus)}"
    );
  }
}
