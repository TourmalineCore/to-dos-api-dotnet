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

    throw new ArgumentOutOfRangeException(
      $"Not expected path of ${nameof(GetStatus)}"
    );
  }
}
