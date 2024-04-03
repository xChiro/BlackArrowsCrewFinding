Feature: A player requests to join a Crew Party and awaits captain's approval.

    Scenario: Join to a Crew Party Successfully
        Given a player named <UserName>
        And a Crew Party already exists
        When the player wants to join to the CrewParty
        Then the player is joined to the CrewParty successfully

    Examples:
      | UserName |
      | Allan    |

    Scenario: Joining a Crew Party that is full
        Given a player named <UserName>
        And a Crew Party named <CrewPartyName> exists
        And <CrewPartyName> has reached its maximum number of crew members
        When the player attempts to join <CrewPartyName>
        Then the player is not joined to the Crew Party

    Examples:
      | UserName | CrewPartyName      |
      | Allan    | Rowan's Crew Party |

    Scenario: Attempting to join a non-existent Crew Party
        Given a player named <UserName>
        When the player attempts to join a Crew Party that does not exist
        Then the player is not joined to the Crew Party

    Examples:
      | UserName | CrewPartyName      |
      | Allan    | Rowan's Crew Party |

    Scenario: Player in a Crew Party trying to join another
        Given a player named <UserName>
        And the player is already a member of a Crew Party
        When the player attempts to join another Crew Party
        Then the player is not joined to the Crew Party

    Examples:
      | UserName |
      | Allan    |