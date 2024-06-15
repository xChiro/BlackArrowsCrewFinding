Feature: Players are able to update their handler name in the settings menu.

    Scenario: As a player, I want to be able to update my handler name.
        Given a player named "Rowan"
        When the player updates their handler name to "Theren"
        Then the player's handler name is "Theren"

    Scenario: Attempting to update a handler name with invalid length
        Given a player named "Rowan"
        When the player updates their handler name to "Th"
        Then the player's handler name is "Rowan"
        And the player is notified that the handler name must be between 3 and 16 characters long

    Examples:
      | newHandlerName                       |
      | "Th"                                 |
      | ""                                   |
      | "This is a really long handler name" |