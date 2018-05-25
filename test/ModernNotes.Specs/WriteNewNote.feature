Feature: Write New Note
	As a user
	I want to write a note and save it

Scenario: Write a new note
	Given I have started the client app
	And I have pressed the New button
	When I write a new note
	And I press the Save button
	Then the list of notes should contain 1 note
