Feature: UsersOverview
	In order to be able to see all available coworkers
	As an Employee
	I want to see all signed in users

Scenario: List all users
	Given User admin exist
	When I list all users
	Then I should see all available users
