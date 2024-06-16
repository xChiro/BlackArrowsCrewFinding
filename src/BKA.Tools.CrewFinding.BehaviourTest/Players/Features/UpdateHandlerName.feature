Feature: Players are able to update their handler name in the settings menu.

    Background:
        Given the StarCitizen Handle must be between "3" and "16" characters in length

    Scenario: As a player, I want to be able to update my handler name.
        Given I am a player named "Rowan"
        When the player updates their handler name to "Theren"
        Then the player's handler name is "Theren"

    Scenario: Attempting to update a handler name with invalid length
        Given I am a player named "Rowan"
        When the player attempts to update their handler name to <newHandlerName>
        Then the player is notified that the handler name has an invalid length
        And the player's handler name is not updated

    Examples:
      | newHandlerName                     |
      | Th                                 |
      |                                    |
      | This is a really long handler name |