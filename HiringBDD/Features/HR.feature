Feature: HR
	In order to maintain employees
	As a HR manager
	I want to be able to manage employees

Background: 
	Given HR exists
	And HR is logged in

Scenario: HR adds new employee
	When HR adds new employee
	Then New employee should be successfully logged in

Scenario: HR change type of an employee
	Given User admin exist
	When HR change type of an employee
	Then Type of an employee should be successfully changed