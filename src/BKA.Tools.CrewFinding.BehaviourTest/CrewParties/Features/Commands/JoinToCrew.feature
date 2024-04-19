Feature: A player requests to join a Crew and awaits captain's approval.

    Scenario: Join to a Crew Successfully
        Given a player named Rowan
        And an existing Crew from other player
        When the player wants to join to the Crew
        Then the player is joined to the Crew successfully

    Scenario: Joining a Crew that is full
        Given a player named Rowan
        And an existing Crew at maximum capacity from other player
        When the player attempts to join the Crew
        Then the player is not joined to the Crew

    Scenario: Attempting to join a non-existent Crew
        Given a player named Allan
        And there is not a Crew
        When the player attempts to join the Crew
        Then the player is not joined to the Crew

    Scenario: Player in a Crew trying to join another
        Given a player named Allan
        And the player is already a member of a Crew
        When the player attempts to join the Crew
        Then the player is not joined to the Crew