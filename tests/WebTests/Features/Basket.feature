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

  Scenario: Authenticated shopper successfully completes checkout
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper logs in as "demouser@microsoft.com" with password "Pass@word1" returning to "/Basket/Checkout"
    And the shopper posts the first item to checkout with quantity "1"
    Then the last request URL should contain "/Basket/Success"
    And the basket page should show "Thanks for your Order!"

  Scenario: Adding two different catalog items keeps both in the basket
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper adds catalog item "3" named "shirt" to the basket
    Then the basket page should show ".NET Black &amp; White Mug"
    And the basket page should show "Prism White T-Shirt"

  Scenario: Updating an item quantity to a larger number reflects in the basket total
    Given the shopper has loaded the home page
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper updates the first item quantity to "49"
    Then the basket page should show "416.50"

  Scenario: Posting checkout with an empty basket redirects back to the basket page
    Given the shopper has loaded the home page
    When the shopper logs in as "demouser@microsoft.com" with password "Pass@word1" returning to "/Basket/Checkout"
    And the shopper posts to checkout
    Then the last request URL should end with "/Basket"
    And the basket page should show "Basket is empty"

  Scenario: An empty basket page renders the empty state for a new anonymous shopper
    When the shopper visits "/Basket/Index"
    Then the basket page should show "Basket is empty"

  Scenario: Visiting checkout while unauthenticated redirects to login
    When the shopper visits "/Basket/Checkout"
    Then the last request URL should contain "/Identity/Account/Login"

  Scenario: An authenticated shopper can add an item to the basket
    Given the shopper has loaded the home page
    When the shopper logs in as "demouser@microsoft.com" with password "Pass@word1" returning to "/"
    And the shopper adds catalog item "2" named "shirt" to the basket
    Then the basket page should show ".NET Black &amp; White Mug"

  Scenario: Removing one item from a multi-item basket keeps the other items
    Given the shopper has loaded the home page
    And the shopper added catalog item "3" named "shirt" to the basket
    And the shopper added catalog item "2" named "shirt" to the basket
    When the shopper updates the first item quantity to "0"
    Then the basket page should show ".NET Black &amp; White Mug"
    And the basket page should not show "Prism White T-Shirt"
