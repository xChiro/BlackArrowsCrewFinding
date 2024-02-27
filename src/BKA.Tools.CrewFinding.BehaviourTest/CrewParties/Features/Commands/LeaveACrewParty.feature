Feature: A player has the ability to leave a Crew Party.

    Scenario: Successful departure from a Crew Party
        Given a player named <UserName>
        And the player is a member of a Crew Party named <CrewPartyName>
        When the player requests to leave <CrewPartyName>
        Then the player is removed from the Crew Party
        And the player receives a confirmation message "You have successfully left <CrewPartyName>."

    Examples:
      | UserName | CrewPartyName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Attempt to leave a Crew Party the player is not part of
        Given a player named <UserName>
        And the player is not a member of any Crew Party named <CrewPartyName>
        When the player attempts to leave <CrewPartyName>
        Then the player remains not part of <CrewPartyName>
        And the player receives a message "You are not a member of <CrewPartyName>."

    Examples:
      | UserName | CrewPartyName         |
      | Rowan    | The Galactic Voyagers |

    Scenario: Captain attempts to leave the Crew Party
        Given a player named <UserName>
        And the player is the captain of a Crew Party named <CrewPartyName>
        When the captain attempts to leave <CrewPartyName>
        Then the captain is not removed from the Crew Party
        And the captain receives a message "As the captain, you cannot leave <CrewPartyName>. You must disband the party first."

    Examples:
      | UserName | CrewPartyName      |
      | Rowan    | The Space Pioneers |