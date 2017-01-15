Feature: CEO collaborating with outsourcing companies
	In order to my company's projects can be done
	As a CEO
	I want to be able to collaborate with outsourcing companies

Background: 
	Given CEO exists
	And CEO is logged in 

Scenario: CEO send request to OS company
	Given CEO get list of non-partner companies
	When CEO send request to one
	Then Request should be successfully sent

Scenario: CEO approve project
	Given new project is created
	When CEO approve project
	Then Project should be successfully approved

Scenario: CEO can see all projects with user stories
	Then CEO should see all projects