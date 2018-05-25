Feature: Update Note
	As a user
	I want to change a previously written note

Scenario: Update a note
	Given I have the following notes saved
	| Id | Text                      |
	| 1  | This is note #1           |
	| 2  | This is the second note   |
	| 3  | And this is note number 3 |
	And I have started the client app
	When I select a note with the text "This is note #1"
	And I press the Edit button
	And I change the text of the note to "This has changed"
	And I press the Save button
	Then the list of notes should contain the following
	| Id | Text                      |
	| 1  | This has changed          |
	| 2  | This is the second note   |
	| 3  | And this is note number 3 |
