Feature: PasswordChange
	In order to have safe access to account
	As an employee
	I want to receive alert when my password expires

@mytag
Scenario: Employee receive alert about his password expiration
	Given User admin exist
	And User's password is expired
	Then I should receive alert