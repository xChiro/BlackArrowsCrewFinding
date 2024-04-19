Feature: A captain has the ability to disband a Crew.

    Scenario: Captain disbands the Crew
        Given a player named <UserName> is the captain of a Crew named <CrewName>
        And the Crew has members
        When <UserName> decides to disband <CrewName>
        Then the Crew <CrewName> is disbanded
        And <UserName> receives a confirmation message 'Crew <CrewName> has been successfully disbanded.'

    Examples:
      | UserName | CrewName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Captain attempts to disband a non-existent Crew
        Given a player named <UserName> is identified as the captain
        And no Crew named <CrewName> exists
        When <UserName> attempts to disband <CrewName>
        Then the system does not find <CrewName> to disband

    Examples:
      | UserName | CrewName     |
      | Rowan    | The Phantom Fleet |