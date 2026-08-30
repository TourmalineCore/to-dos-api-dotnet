Feature: Hello World
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
