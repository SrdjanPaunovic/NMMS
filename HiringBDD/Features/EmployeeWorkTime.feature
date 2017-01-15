Feature: EmployeeWorkTime
	In order to be able to plan my day better
	As an employer
	I want to be able to define my working time

@mytag
Scenario: Employee define work time
	Given User admin exist
	And I am logged in
	When I change my work time
	Then My work time should be successfully changed