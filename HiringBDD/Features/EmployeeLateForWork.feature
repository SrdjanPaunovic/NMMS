Feature: EmployeeLateForWork
	In order to have feedback about my correctness at work
	As an Employee
	I want to receive alert

Scenario: Employee receive alert
	Given User admin exist
	And I am logged in
	Then I should receive alert
