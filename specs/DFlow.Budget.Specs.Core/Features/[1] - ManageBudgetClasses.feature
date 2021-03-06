﻿Feature: Feature - 1 - ManageBudgetClasses
	As a master user
	I need to manage budget classes
	To keep control of my budget

Scenario: Scenario - 1.1 - Add budget classes

	Given we are working with tenant "1.1 - Add budget classes" which has no data

	When I add budget classes:
		| Name           | SortOrder | TransactionType |
		| Income         | 1         | Income          |
		| Housing        | 2         | Expense         |
		| Food           | 3         | Expense         |
		| Transportation | 4         | Expense         |
		| Entertainment  | 5         | Expense         |

	Then I can view the following budget classes;
		| Name           | SortOrder | TransactionType |
		| Income         | 1         | Income          |
		| Housing        | 2         | Expense         |
		| Food           | 3         | Expense         |
		| Transportation | 4         | Expense         |
		| Entertainment  | 5         | Expense         |

