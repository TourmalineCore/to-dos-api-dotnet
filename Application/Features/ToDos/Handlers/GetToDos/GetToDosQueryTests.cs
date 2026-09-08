using Core.Entities;
using Xunit;

namespace Application.Features.ToDos.Handlers.GetToDos;

public class GetToDosQueryTests : IntegrationTestBase
{
  [Fact]
  public async Task GetAsync_ShouldOrderToDosThatNewerAreFirst()
  {
    var context = CreateAppDbContext();

    var firstToDo = new ToDo { Name = "First" };
    await AddEntityAndSaveAsync(context, firstToDo);

    var secondToDo = new ToDo { Name = "Second" };
    await AddEntityAndSaveAsync(context, secondToDo);

    var thirdToDo = new ToDo { Name = "Third" };
    await AddEntityAndSaveAsync(context, thirdToDo);

    var getToDosQuery = new GetToDosQuery(context);

    var toDos = await getToDosQuery.GetAsync();

    Assert.Equal(thirdToDo.Name, toDos[0].Name);
    Assert.Equal(secondToDo.Name, toDos[1].Name);
    Assert.Equal(firstToDo.Name, toDos[2].Name);
  }
}
