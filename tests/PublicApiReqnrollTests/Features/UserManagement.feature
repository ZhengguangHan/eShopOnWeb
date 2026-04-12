Feature: User Management
  As an administrator
  I want to manage users
  So that I can control user accounts

  # --- List ---

  Scenario: List users as admin
    Given I am authenticated as an "admin"
    When I request the list of users
    Then the response status code should be 200
    And the response should contain users

  Scenario: List users as anonymous returns unauthorized
    Given I am an anonymous user
    When I request the list of users
    Then the response status code should be 401

  Scenario: List users as normal user returns forbidden
    Given I am authenticated as a "normal user"
    When I request the list of users
    Then the response status code should be 403

  Scenario: List users as product manager returns forbidden
    Given I am authenticated as a "product manager"
    When I request the list of users
    Then the response status code should be 403

  # --- Create ---

  Scenario: Create user as admin
    Given I am authenticated as an "admin"
    When I create a user with username "newuser@microsoft.com"
    Then the response status code should be 201
    And the created user should have an id

  Scenario: Create duplicate user returns conflict
    Given I am authenticated as an "admin"
    When I create a user with username "admin@microsoft.com"
    Then the response status code should be 409

  Scenario: Create user with missing username returns bad request
    Given I am authenticated as an "admin"
    When I create a user with missing username
    Then the response status code should be 400

  Scenario: Create user as normal user returns forbidden
    Given I am authenticated as a "normal user"
    When I create a user with username "forbidden@microsoft.com"
    Then the response status code should be 403

  # --- Delete ---

  Scenario: Delete admin user returns bad request
    Given I am authenticated as an "admin"
    When I get the user id for username "admin@microsoft.com"
    And I delete user by that id
    Then the response status code should be 400

  Scenario: Delete user with invalid id returns not found
    Given I am authenticated as an "admin"
    When I delete user by id "00000000-0000-0000-0000-000000000000"
    Then the response status code should be 404

  Scenario: Delete user as normal user returns forbidden
    Given I am authenticated as a "normal user"
    When I delete user by id "00000000-0000-0000-0000-000000000000"
    Then the response status code should be 403

  # --- Get User Roles ---

  Scenario: Get roles for admin user
    Given I am authenticated as an "admin"
    When I get the user id for username "admin@microsoft.com"
    And I request roles for that user
    Then the response status code should be 200
    And the user roles should contain "Administrators"

  Scenario: Get roles for invalid user returns not found
    Given I am authenticated as an "admin"
    When I request roles for user id "00000000-0000-0000-0000-000000000000"
    Then the response status code should be 404
