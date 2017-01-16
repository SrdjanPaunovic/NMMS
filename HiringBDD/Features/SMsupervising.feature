Feature: SM supervising
	In order to make myself sure that project are done correctly
	As a Scram master
	I want to be able to supervise company's projects

Background: 
	Given SM exists
	And SM is logged in

Scenario: SM receive alert about deadline
	Given Project is created
	When Project is close to deadline
	Then SM should receive alert