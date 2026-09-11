using Application.Features.ToDos.Handlers.CreateToDo;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.Features.ToDos;

public class ToDosControllerValidationTests(
  WebApplicationFactory<Program> factory
) : ControllerValidationTestsBase(factory)
{
  [Theory]
  [MemberData(nameof(CreateToDoReuqestInvalidNamesTestCases))]
  public async Task CreateToDoAsync_ShouldThrowBadRequestIfNameIsInvalid(
    string name
  )
  {
    var createToDoRequest = new CreateToDoRequest { Name = name };

    var response = await _httpClient.PostAsJsonAsync(
      "/to-dos",
      createToDoRequest
    );

    Assert.NotNull(response);
    Assert.Equal(
      StatusCodes.Status400BadRequest,
      (int)response.StatusCode
    );
  }

  public static IEnumerable<object[]> CreateToDoReuqestInvalidNamesTestCases =>
    [
      [null!],
      [""],
      [new string('x', 257)],
    ];
}
