Feature: Hello World
  Background:
    * header Content-Type = 'application/json'

  Scenario: Happy Path
    Given url 'http://localhost:5200/weatherforecast'
    And method GET
    Then status 200
    And match response == '#[5]'
