using Core.Entities;
using Xunit;

namespace Application.Features.ToDos.Handlers.GetToDos;

public class GetToDosQueryTests : IntegrationTestBase
{
  [Fact]
  public async Task GetAsync_ShouldOrderToDosThatNewerAreFirst()
  {
    var context = CreateAppDbContext();

    var dt = new DateTime(2026, 01, 02, 03, 04, 05, 678, DateTimeKind.Utc);

    var firstToDo = new ToDo
    {
      Name = "First",
      CreatedAtUtc = dt.AddHours(1),
    };
    await AddEntityAndSaveAsync(context, firstToDo);

    var secondToDo = new ToDo
    {
      Name = "Second",
      CreatedAtUtc = dt.AddHours(2),
    };
    await AddEntityAndSaveAsync(context, secondToDo);

    var thirdToDo = new ToDo
    {
      Name = "Third",
      CreatedAtUtc = dt.AddHours(3),
    };
    await AddEntityAndSaveAsync(context, thirdToDo);

    var getToDosQuery = new GetToDosQuery(context);

    var toDos = await getToDosQuery.GetAsync();

    Assert.Equal(thirdToDo.Name, toDos[0].Name);
    Assert.Equal(secondToDo.Name, toDos[1].Name);
    Assert.Equal(firstToDo.Name, toDos[2].Name);
  }
}
