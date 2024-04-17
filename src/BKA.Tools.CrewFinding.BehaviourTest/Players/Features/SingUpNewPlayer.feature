Feature: SingUp new players

    Scenario: As a new player, I want to sign up as a first-time user
        Given I am a player who does not have a player profile with the following UserId "1"
        When I attempt to create a new player profile with StarCitizen Handle "Rowan"
        Then I should have a player profile created with UserId "1" and StarCitizen Handle "Rowan"

    Scenario: As a new player, I cannot sign up with an empty UserId
        Given I am a player who does not have a player profile with empty UserId
        When I attempt to create a new player profile with a StarCitizen Handle "Rowan"
        Then I should receive an error message that the UserId cannot be empty

    Scenario: As a new player, I cannot sign up with an empty StarCitizen Handle
        Given I am a player who does not have a player profile with the following UserId "1"
        When I attempt to create a new player profile with an empty StarCitizen Handle
        Then I should receive an error message that the StarCitizen Handle cannot be empty

    Scenario: As a new player, I cannot sign up if the StarCitizen Handle length is invalid
        Given I am a player who does not have a player profile with the following UserId "1"
        And the StarCitizen Handle must be between "3" and "30" characters in length
        When I attempt to create a new player profile with UserId "<UserId>" and StarCitizen Handle "<starCitizenHandle>"
        Then I should receive an error message that the StarCitizen Handle length is invalid

    Examples:
      | UserId | starCitizenHandle                                      |
      | 1      | R                                                      |
      | 1      | Ro                                                     |
      | 1      | A very very very very very long name should be invalid |