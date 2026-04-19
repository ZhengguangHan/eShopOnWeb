Feature: Order Detail Page
  As a shopper
  I want to see product images and line-item subtotals on the order detail page
  And I want a print-friendly layout so my order confirmation prints cleanly

  Scenario: The order detail page shows product images without the mobile-hide class
    Given the shopper has placed an order for catalog item "2" named "shirt"
    When the shopper opens the detail page for their latest order
    Then the order detail page should show "esh-orders-detail-image"
    And the order detail page should not contain an image column with class "hidden-md-down"

  Scenario: The order detail page renders column headers for the order items table
    Given the shopper has placed an order for catalog item "2" named "shirt"
    When the shopper opens the detail page for their latest order
    Then the order detail page should show "Product"
    And the order detail page should show "Quantity"
    And the order detail page should show "Subtotal"

  Scenario: The order detail page shows the per-line subtotal
    Given the shopper has placed an order for catalog item "2" named "shirt"
    When the shopper opens the detail page for their latest order
    Then the order detail page should show "$ 8.50"

  Scenario: The orders stylesheet exposes print-friendly rules
    When the shopper requests the orders stylesheet
    Then the orders stylesheet should contain "@media print"
    And the orders stylesheet should contain "page-break-inside"
    And the orders stylesheet should contain ".esh-header"
