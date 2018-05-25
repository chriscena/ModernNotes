Feature: View Previous Notes
	As a user
	I want to view all my previous notes

Scenario: View saved notes
	Given I have the following notes saved
	| Id | Text                      |
	| 1  | This is note #1           |
	| 2  | This is the second note   |
	| 3  | And this is note number 3 |
	When I have started the client app
	Then the list of notes should contain the following
	| Id | Text                      |
	| 1  | This is note #1           |
	| 2  | This is the second note   |
	| 3  | And this is note number 3 |
