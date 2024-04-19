Feature: A player requests to join a Crew Party and awaits captain's approval.

    Scenario: Join to a Crew Party Successfully
        Given a player named Rowan
        And an existing Crew Party from other player
        When the player wants to join to the CrewParty
        Then the player is joined to the CrewParty successfully

    Scenario: Joining a Crew Party that is full
        Given a player named Rowan
        And an existing Crew Party at maximum capacity from other player
        When the player attempts to join the Crew Party
        Then the player is not joined to the Crew Party

    Scenario: Attempting to join a non-existent Crew Party
        Given a player named Allan
        And there is not a Crew Party
        When the player attempts to join the Crew Party
        Then the player is not joined to the Crew Party

    Scenario: Player in a Crew Party trying to join another
        Given a player named Allan
        And the player is already a member of a Crew Party
        And an existing Crew Party from other player
        When the player attempts to join another Crew Party
        Then the player is not joined to the Crew Party