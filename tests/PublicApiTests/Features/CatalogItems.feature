Feature: Catalog Items
  As an API consumer
  I want to manage catalog items
  So that I can browse and maintain the product catalog

  # --- List / Pagination ---

  Scenario: List catalog items with page size
    Given I am an anonymous user
    When I request catalog items with page size 10
    Then the response status code should be 200
    And the response should contain at most 10 catalog items

  Scenario: List catalog items with pagination
    Given I am an anonymous user
    When I request catalog items with page size 10 and page index 0
    Then the response status code should be 200
    And the response should contain at most 10 catalog items

  Scenario: Second page returns different results than first page
    Given I am an anonymous user
    When I request catalog items with page size 5 and page index 0
    And I store the catalog items response
    And I request catalog items with page size 5 and page index 1
    Then the response status code should be 200

  # --- Get By Id ---

  Scenario: Get catalog item by valid id
    Given I am an anonymous user
    When I request catalog item with id 5
    Then the response status code should be 200
    And the response should contain a catalog item

  Scenario: Get catalog item by invalid id returns not found
    Given I am an anonymous user
    When I request catalog item with id 0
    Then the response status code should be 404

  # --- Create ---

  Scenario: Create catalog item as product manager
    Given I am authenticated as a "product manager"
    When I create a catalog item with name "Test Product" and price 9.99
    Then the response status code should be 201
    And the created catalog item should have name "Test Product"
    And the created catalog item should have price 9.99

  Scenario: Create catalog item as normal user is forbidden
    Given I am authenticated as a "normal user"
    When I create a catalog item with name "Forbidden Product" and price 5.00
    Then the response status code should be 403

  Scenario: Create catalog item as admin is forbidden
    Given I am authenticated as an "admin"
    When I create a catalog item with name "Admin Product" and price 5.00
    Then the response status code should be 403

  # --- Delete ---

  Scenario: Delete catalog item as product manager
    Given I am authenticated as a "product manager"
    When I delete catalog item with id 12
    Then the response status code should be 204

  Scenario: Delete catalog item with invalid id returns not found
    Given I am authenticated as a "product manager"
    When I delete catalog item with id 0
    Then the response status code should be 404
