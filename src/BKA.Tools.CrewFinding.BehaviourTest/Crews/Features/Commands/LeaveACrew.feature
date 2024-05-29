Feature: A player has the ability to leave a Crew.

    Scenario: Successful departure from a Crew
        Given a player named <UserName>
        And the player is a member of a Crew
        When the player requests to leave the Crew
        Then the player is removed from the Crew

    Examples:
      | UserName |
      | Rowan    |
    Scenario: Attempt to leave a Crew the player is not part of
        Given a player named <UserName>
        And the player is not a member of any Crew
        When the player attempts to leave the Crew
        Then the player receives a message indicating the player is not a member of the Crew

    Examples:
      | UserName |
      | Rowan    |
      
    Scenario: Captain attempts to leave the Crew
        Given a player named <UserName>
        And the player is the captain of a Crew
        When the captain attempts to leave the Crew
        Then the captain is not removed from the Crew

    Examples:
      | UserName |
      | Rowan    |