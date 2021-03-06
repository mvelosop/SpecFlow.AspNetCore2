﻿Feature: Feature - 1 - ManageBudgetClasses
    As a master user
    I need to manage budget classes
    To keep control of my budget

Scenario: Scenario - 1.1 - Add budget classes

    Given we are working with a new scenario tenant context

    When I add budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |
        | Transportation | 4         | Expense         |
        | Entertainment  | 5         | Expense         |

    Then I get the following budget classes
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


Scenario: Scenario - 1.3 - Modify budget classes

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |

    When I modify the original budget classes:
        | FindName | Name                       | SortOrder | TransactionType |
        | Income   | Income - Updated           | 1         | Income          |
        | Housing  | Housing - Update SortOrder | 3         | Expense         |
        | Food     | Food - Update Type         | 3         | Investment      |

    Then I get the following budget classes
        | Name                       | SortOrder | TransactionType |
        | Income - Updated           | 1         | Income          |
        | Housing - Update SortOrder | 3         | Expense         |
        | Food - Update Type         | 3         | Investment      |


Scenario: Scenario - 1.4 - Remove budget classes

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |

    When I delete the original budget classes:
        | FindName |
        | Housing  |

    Then I get the following budget classes
        | Name   | SortOrder | TransactionType |
        | Income | 1         | Income          |
        | Food   | 3         | Expense         |

Scenario: Scenario - 1.5 - Add budget items

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |

    When I add the following budget items:
        | BudgetClass | Name        | SortOrder | BaseAmount |
        | Income      | Job         | 1         | 3000       |
        | Housing     | Rent        | 1         | 1000       |
        | Housing     | Phone       | 2         | 62         |
        | Housing     | Electricity | 3         | 44         |
        | Housing     | Water       | 4         | 8          |

    Then I get the following budget items for class "Housing":
        | Name        | SortOrder | BaseAmount | Percent |
        | Rent        | 1         | 1000       | 89,77   |
        | Phone       | 2         | 62         | 5,57    |
        | Electricity | 3         | 44         | 3,95    |
        | Water       | 4         | 8          | 0,72    |


Scenario: Scenario - 1.6 - Update budget items

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |

    And I've added the following budget items:
        | BudgetClass | Name        | SortOrder | BaseAmount |
        | Housing     | Rent        | 1         | 1000       |
        | Housing     | Phone       | 2         | 62         |
        | Housing     | Electricity | 3         | 44         |
        | Housing     | Water       | 4         | 8          |
        | Housing     | Cable       | 5         | 34         |

    When I update budget class "Housing" to this item list:
        | FindName    | Name                             | SortOrder | BaseAmount |
        | Rent        | Rent - Updated Name              | 1         | 1000       |
        | Phone       | Phone - Updated SortOrder        | 2         | 62         |
        | Electricity | Electricity - Updated BaseAmount | 4         | 100        |
        | Water       | Water                            | 3         | 8          |
        |             | Waste removal                    | 6         | 10         |

    Then I get the following budget items for class "Housing":
        | Name                             | SortOrder | BaseAmount | Percent |
        | Rent - Updated Name              | 1         | 1000       | 84,75   |
        | Phone - Updated SortOrder        | 2         | 62         | 5,25    |
        | Electricity - Updated BaseAmount | 4         | 100        | 8,47    |
        | Water                            | 3         | 8          | 0,68    |
        | Waste removal                    | 6         | 10         | 0,85    |


Scenario: Scenario - 1.7 - Can't duplicate budget item names

    Given we are working with a new scenario tenant context

    And I've added budget classes:
        | Name           | SortOrder | TransactionType |
        | Income         | 1         | Income          |
        | Housing        | 2         | Expense         |
        | Food           | 3         | Expense         |

    And I've added the following budget items:
        | BudgetClass | Name        | SortOrder | BaseAmount |
        | Housing     | Rent        | 1         | 1000       |
        | Housing     | Phone       | 2         | 62         |

    Then I can't update budget class "Housing" to duplicate names as of this item list:
        | FindName | Name | SortOrder | BaseAmount |
        | Rent     | Rent | 1         | 1000       |
        | Phone    | Rent | 2         | 62         |

    Then I can't update budget class "Housing" to duplicate names as of this item list:
        | FindName | Name | SortOrder | BaseAmount |
        | Rent     | Rent | 1         | 1000       |
        |          | Rent | 2         | 62         |
