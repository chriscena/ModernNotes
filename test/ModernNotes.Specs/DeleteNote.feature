Feature: Delete Note
	As a user
	I want to delete a previously written note.

Scenario: Delete note
	Given I have the following notes saved
	| Id | Text                      |
	| 1  | This is note #1           |
	| 2  | This is the second note   |
	| 3  | And this is note number 3 |
	And I have started the client app
	When I select a note with the text "This is the second note"
	And I press the Delete button
	Then the list of notes should contain the following
	| Id | Text                      |
	| 1  | This is note #1           |
	| 3  | And this is note number 3 |
