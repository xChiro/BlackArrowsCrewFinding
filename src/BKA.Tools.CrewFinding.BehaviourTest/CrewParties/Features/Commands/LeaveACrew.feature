Feature: A player has the ability to leave a Crew.

    Scenario: Successful departure from a Crew
        Given a player named <UserName>
        And the player is a member of a Crew named <CrewName>
        When the player requests to leave <CrewName>
        Then the player is removed from the Crew
        And the player receives a confirmation message "You have successfully left <CrewName>."

    Examples:
      | UserName | CrewName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Attempt to leave a Crew the player is not part of
        Given a player named <UserName>
        And the player is not a member of any Crew named <CrewName>
        When the player attempts to leave <CrewName>
        Then the player remains not part of <CrewName>
        And the player receives a message "You are not a member of <CrewName>."

    Examples:
      | UserName | CrewName         |
      | Rowan    | The Galactic Voyagers |

    Scenario: Captain attempts to leave the Crew
        Given a player named <UserName>
        And the player is the captain of a Crew named <CrewName>
        When the captain attempts to leave <CrewName>
        Then the captain is not removed from the Crew
        And the captain receives a message "As the captain, you cannot leave <CrewName>. You must disband the party first."

    Examples:
      | UserName | CrewName      |
      | Rowan    | The Space Pioneers |