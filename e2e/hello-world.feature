Feature: Hello World
  Background:
    * header Content-Type = 'application/json'

  Scenario: Happy Path
    Given url 'https://google.com'
    When method GET
    Then status 200
