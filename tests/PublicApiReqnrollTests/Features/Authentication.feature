Feature: Authentication
  As an API consumer
  I want to authenticate with the API
  So that I can access protected endpoints

  Scenario: Successful authentication with valid credentials
    Given I am an anonymous user
    When I authenticate with username "demouser@microsoft.com" and password "Pass@word1"
    Then the response status code should be 200
    And the authentication result should be true
    And the response should contain a token

  Scenario: Failed authentication with wrong password
    Given I am an anonymous user
    When I authenticate with username "demouser@microsoft.com" and password "badpassword"
    Then the response status code should be 200
    And the authentication result should be false

  Scenario: Failed authentication with non-existent user
    Given I am an anonymous user
    When I authenticate with username "baduser@microsoft.com" and password "badpassword"
    Then the response status code should be 200
    And the authentication result should be false
