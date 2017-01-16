Feature: Login
	In order to access my account
	As an Employee
	I want to be able to log in into system

	Background: 
	Given User admin exist

@mytag
Scenario Outline: Valid login
	Given I enter valid "<username>" or "<password>"
	When I click Log in
	Then I should be successfully logged in

	Examples: 
	| username | password |
	| admin    | admin    |

Scenario Outline: Invalid login
	Given I enter invalid "<username>" or "<password>"
	When I click Log in
	Then I should get an login error

	Examples: 
	| username | password |
	| admin    | hej      |
	| hej      | admin    |
	|          |          |
