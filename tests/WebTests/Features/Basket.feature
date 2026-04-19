Feature: Basket Web Pages
  As a shopper
  I want the basket pages to reliably add, update, transfer, and check out items
  So that my shopping experience is consistent across sessions

  Scenario: An anonymous shopper can add an item to the basket
    Given the shopper has loaded the home page
    When the shopper adds catalog item "2" named "shirt" to the basket
    Then the basket page should show ".NET Black &amp; White Mug"

  Scenario: Adding the same item twice increments the quantity
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper adds catalog item "2" named "shirt" to the basket
    Then the basket should show quantity "2" for the first item

  Scenario: Updating an item quantity to zero empties the basket
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper updates the first item quantity to "0"
    Then the basket page should show "Basket is empty"

  Scenario: Anonymous basket is preserved after login
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper logs in as "demouser@microsoft.com" with password "Pass@word1" returning to "/Basket/Index"
    Then the basket page should show ".NET Black &amp; White Mug"

  Scenario: Checkout redirects unauthenticated shopper to login
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper posts to checkout
    Then the last request URL should contain "/Identity/Account/Login"
