Feature: ChangeProfile
	In order to my informations are up to date
	As an Employee
	I want to be able to change my profile informations

Scenario: Employee change profile info
	Given User admin exist
	When I change my profile
	Then My profile should be changed
