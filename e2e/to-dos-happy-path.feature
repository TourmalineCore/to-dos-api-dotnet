Feature: To Dos
  Background:
    * header Content-Type = 'application/json'

    * def jsUtils = read('./js-utils.js')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')

  Scenario: Happy Path
    * def randomToDoName = '[API-E2E]-Test-to-do-' + Math.random()
    
    Given url apiRootUrl
    And path 'to-dos'
    And request
    """
    {
      "name": "#(randomToDoName)"
    }
    """
    When method POST
    Then status 200

    * def newToDoId = response.newToDoId

    Given url apiRootUrl
    And path 'to-dos'
    When method GET
    Then match response.toDos contains
    """
    {
      "id": "#(newToDoId)",
      "name": "#(randomToDoName)",
      "status": "New",
    }
    """

    Given url apiRootUrl
    Given path 'to-dos'
    And params { toDoId: "#(newToDoId)" }
    When method DELETE
    Then match response == { isDeleted: true }

    Given url apiRootUrl
    Given path 'to-dos'
    When method GET
    Then assert response.toDos.filter(x => x.id == newToDoId).length == 0
