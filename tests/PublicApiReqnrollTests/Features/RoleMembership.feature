Feature: Role Membership
  As an administrator
  I want to manage role memberships
  So that I can assign and remove users from roles

  # --- Get Members ---

  Scenario: Get members of a valid role
    Given I am authenticated as an "admin"
    When I request members of role "Administrators"
    Then the response status code should be 200
    And the response should contain role members

  Scenario: Get members of an invalid role returns empty list
    Given I am authenticated as an "admin"
    When I request members of role "NonExistentRole"
    Then the response status code should be 200
    And the response should contain no role members

  # --- Remove Member ---

  Scenario: Remove user from role successfully
    Given I am authenticated as an "admin"
    When I get the id of role "Product Managers"
    And I get a member of role "Product Managers"
    And I remove that user from that role
    Then the response status code should be 204

  Scenario: Remove user from role with invalid user id returns not found
    Given I am authenticated as an "admin"
    When I get the id of role "Administrators"
    And I remove user "00000000-0000-0000-0000-000000000000" from that role
    Then the response status code should be 404

  Scenario: Remove user from role with invalid role id returns not found
    Given I am authenticated as an "admin"
    When I remove user from role with invalid role id
    Then the response status code should be 404
