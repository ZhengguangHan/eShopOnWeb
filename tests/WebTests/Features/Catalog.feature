Feature: Catalog Detail Page
  As a shopper
  I want to view a product's full details before buying
  So that I can make an informed purchase decision

  Scenario: Detail page renders the product name and price
    When the shopper visits "/Catalog/2"
    Then the response status should be "OK"
    And the catalog page should show ".NET Black &amp; White Mug"
    And the catalog page should show "8.50"

  Scenario: Detail page renders the description region
    When the shopper visits "/Catalog/2"
    Then the catalog page should show "esh-catalog-description"

  Scenario: Detail page renders the product image
    When the shopper visits "/Catalog/2"
    Then the catalog page should contain an image tag for catalog item "2"

  Scenario: Detail page exposes an Add to Basket button
    When the shopper visits "/Catalog/2"
    Then the catalog page should show "Add to Basket"

  Scenario: Adding to basket from the detail page redirects to the basket
    Given the shopper is on the catalog detail page for item "2"
    When the shopper submits the add-to-basket form on the detail page
    Then the basket page should show ".NET Black &amp; White Mug"

  Scenario: Home page card links to the catalog detail page
    Given the shopper has loaded the home page
    Then the catalog page should contain a link to "/Catalog/2"
    And the catalog page should not contain a direct add-to-basket form

  Scenario: Detail page for an unknown id returns 404
    When the shopper visits "/Catalog/999999"
    Then the response status should be "NotFound"
