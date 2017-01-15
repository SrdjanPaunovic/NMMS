Feature: CEO collaborating with outsourcing companies
	In order to my company's projects can be done
	As a CEO
	I want to be able to collaborate with outsourcing companies

Background: 
	Given CEO exists

Scenario: CEO send request to OS company
	Given Non-partner company exist
	Given CEO get list of non-partner companies
	When CEO send request to one
	Then Request should be successfully sent

Scenario: CEO approve project
	Given new project is created
	When CEO approve project
	Then Project should be successfully approved