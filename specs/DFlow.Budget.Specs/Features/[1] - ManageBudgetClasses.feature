Feature: Feature - 1 - ManageBudgetClasses
    As a master user
    I need to manage budget classes
    To keep control of my budget

Scenario: Scenario - 1.1 - Add budget classes

    Given we are working with tenant "Scenario - 1.1 - Add budget classes" which has no data

    When I add budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |
        | Transportation | 4         | Expense         |
        | Entertainment  | 5         | Expense         |

    Then I can get the following budget classes
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |
        | Transportation | 4         | Expense         |
        | Entertainment  | 5         | Expense         |


Scenario: Scenario - 1.2 - Avoid duplicate name in budget classes

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |

    Then I can't duplicate budget class names:
        | Name           | SortOrder | TransactionType |
        | Income         | 2         | Expense         |

