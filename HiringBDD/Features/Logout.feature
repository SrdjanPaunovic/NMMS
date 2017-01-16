Feature: Logout
	In order to finish my session
	As an Employee
	I want to be able to log out from system

Background: 
	Given User admin exist
	Given I am logged in

Scenario: Log out
	When I click log out
	Then I should be successfully logged out
