Feature: A captain has the ability to disband a Crew Party.

    Scenario: Captain disbands the Crew Party
        Given a player named <UserName> is the captain of a Crew Party named <CrewPartyName>
        And the Crew Party has members
        When <UserName> decides to disband <CrewPartyName>
        Then the Crew Party <CrewPartyName> is disbanded
        And <UserName> receives a confirmation message 'Crew Party <CrewPartyName> has been successfully disbanded.'

    Examples:
      | UserName | CrewPartyName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Captain attempts to disband a non-existent Crew Party
        Given a player named <UserName> is identified as the captain
        And no Crew Party named <CrewPartyName> exists
        When <UserName> attempts to disband <CrewPartyName>
        Then the system does not find <CrewPartyName> to disband

    Examples:
      | UserName | CrewPartyName     |
      | Rowan    | The Phantom Fleet |