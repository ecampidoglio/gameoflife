@RequireWebBrowser
Feature: Game
	In order to play Game of Life in a web browser
	As a user
	I want to be able to see the state of the universe through time

Scenario: Death by under-population
	Given a live cell in the grid does not have any live neighbours
	When I trigger the next generation of cells
	Then the cell in the grid should be dead
