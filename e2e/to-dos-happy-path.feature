Feature: To Dos
  Background:
    * header Content-Type = 'application/json'

    * def jsUtils = read('./js-utils.js')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')

  Scenario: Happy Path
    Given url apiRootUrl
    Given path 'weatherforecast'
    And method GET
    Then status 200
    And match response == '#[5]'

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
    }
    """

    Given path 'to-dos'
    And params { toDoId: "#(newToDoId)" }
    When method DELETE
    Then status 200
    And match response ==
    """
    {
      "isDeleted": true,
    }
    """
