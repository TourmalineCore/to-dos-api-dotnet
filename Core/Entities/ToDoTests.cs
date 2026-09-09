using Moq;
using Xunit;

namespace Core.Entities;

public class ToDoTests
{
  [Fact]
  public async Task GetStatus_FiveDaysAgoIsNew()
  {
    var dt = new DateTime(2026, 01, 02, 03, 04, 05, 678, DateTimeKind.Utc);

    var toDo = new ToDo { Name = "To Do", CreatedAtUtc = dt };

    var dateTimeProviderMock = new Mock<IDateTimeProvider>();

    dateTimeProviderMock.Setup(x => x.UtcNow).Returns(dt.AddDays(5));

    var status = toDo.GetStatus(dateTimeProviderMock.Object);

    Assert.Equal(ToDoStatus.New, status);
  }

  [Fact]
  public async Task GetStatus_EightDaysAgoIsOld()
  {
    var dt = new DateTime(2026, 01, 02, 03, 04, 05, 678, DateTimeKind.Utc);

    var toDo = new ToDo { Name = "To Do", CreatedAtUtc = dt };

    var dateTimeProviderMock = new Mock<IDateTimeProvider>();

    dateTimeProviderMock.Setup(x => x.UtcNow).Returns(dt.AddDays(8));

    var status = toDo.GetStatus(dateTimeProviderMock.Object);

    Assert.Equal(ToDoStatus.Old, status);
  }
}
