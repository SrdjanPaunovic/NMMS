@BDDHiring
Feature: POManageProject
	In order to PO can manage project
	As a Product Owner
	I want to be able to work with projects

Background: 
	Given PO exists
	And PO is logged in

Scenario: PO create new project
	When PO create new project
	Then Project should be waiting for approvement

Scenario: PO accept user story that TL created
	Given User story is created
	When PO accept user story
	Then User story should be accepted

Scenario: PO reject user story that TL created
	Given User story is created
	When PO reject user story
	Then User story should be rejected
