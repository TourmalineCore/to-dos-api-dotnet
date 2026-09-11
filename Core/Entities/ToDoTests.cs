using Moq;
using Xunit;

namespace Core.Entities;

public class ToDoTests
{
  [Theory]
  [MemberData(nameof(TestCases))]
  public void GetStatus(DateTime utcNow, ToDoStatus expectedStatus)
  {
    var dt = new DateTime(2026, 01, 02, 03, 04, 05, 678, DateTimeKind.Utc);

    var toDo = new ToDo { Name = "To Do", CreatedAtUtc = dt };

    var dateTimeProviderMock = new Mock<IDateTimeProvider>();

    dateTimeProviderMock.Setup(x => x.UtcNow).Returns(utcNow);

    var status = toDo.GetStatus(dateTimeProviderMock.Object);

    Assert.Equal(expectedStatus, status);
  }

  public static IEnumerable<object[]> TestCases =>
    [
      [
        new DateTime(2026, 01, 07, 03, 04, 05, 678, DateTimeKind.Utc),
        ToDoStatus.New,
      ],
      [
        new DateTime(2026, 01, 10, 03, 04, 05, 678, DateTimeKind.Utc),
        ToDoStatus.Old,
      ],
      [
        new DateTime(2026, 01, 30, 03, 04, 05, 678, DateTimeKind.Utc),
        ToDoStatus.Forgotten,
      ],
    ];
}
