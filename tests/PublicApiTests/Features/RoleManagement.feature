Feature: Role Management
  As an administrator
  I want to manage roles
  So that I can control access to the system

  # --- List ---

  Scenario: List roles as admin
    Given I am authenticated as an "admin"
    When I request the list of roles
    Then the response status code should be 200
    And the response should contain roles

  Scenario: List roles as anonymous returns unauthorized
    Given I am an anonymous user
    When I request the list of roles
    Then the response status code should be 401

  Scenario: List roles as normal user returns forbidden
    Given I am authenticated as a "normal user"
    When I request the list of roles
    Then the response status code should be 403

  # --- Create ---

  Scenario: Create role as admin
    Given I am authenticated as an "admin"
    When I create a role with name "TestRole"
    Then the response status code should be 201
    And the created role should have name "TestRole"

  Scenario: Create duplicate role returns conflict
    Given I am authenticated as an "admin"
    When I create a role with name "Administrators"
    Then the response status code should be 409

  Scenario: Create role as normal user returns forbidden
    Given I am authenticated as a "normal user"
    When I create a role with name "ForbiddenRole"
    Then the response status code should be 403

  # --- Get By Id ---

  Scenario: Get role by valid id
    Given I am authenticated as an "admin"
    When I get the first role id from the role list
    And I request role by that id
    Then the response status code should be 200

  Scenario: Get role by invalid id returns not found
    Given I am authenticated as an "admin"
    When I request role by id "00000000-0000-0000-0000-000000000000"
    Then the response status code should be 404

  # --- Delete ---

  Scenario: Delete role that is still assigned returns conflict
    Given I am authenticated as an "admin"
    When I get the id of role "Administrators"
    And I delete role by that id
    Then the response status code should be 409

  Scenario: Delete role by invalid id returns not found
    Given I am authenticated as an "admin"
    When I delete role by id "00000000-0000-0000-0000-000000000000"
    Then the response status code should be 404
