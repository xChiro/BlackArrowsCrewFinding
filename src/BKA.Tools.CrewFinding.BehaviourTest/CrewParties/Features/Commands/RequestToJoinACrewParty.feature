Feature: A player requests to join a Crew Party and awaits captain's approval.

    Scenario: Requesting to join a Crew Party
        Given a player with a RSI Handle of <UserName>
        And a Crew Party named <CrewPartyName> exists
        When the player requests to join <CrewPartyName>
        Then a join request is sent to the captain of <CrewPartyName> with creation date and time of now
        And the player receives a notification Join request sent to the captain of <CrewPartyName>.

    Examples:
      | UserName | CrewPartyName      |
      | Allan    | Rowan's Crew Party |

    Scenario: Joining a Crew Party that is full
        Given a player with a RSI Handle of <UserName>
        And a Crew Party named <CrewPartyName> exists
        And <CrewPartyName> has reached its maximum number of crew members
        When the player attempts to join <CrewPartyName>
        Then the player is not allowed to send a join request
        And the player receives a message Unable to request to join <CrewPartyName> as it is full.

    Examples:
      | UserName | CrewPartyName      |
      | Allan    | Rowan's Crew Party |

    Scenario: Attempting to join a non-existent Crew Party
        Given a player with a RSI Handle of <UserName>
        When the player attempts to join a Crew Party named <CrewPartyName>
        And <CrewPartyName> does not exist
        Then the join request is not sent
        And the player receives a message Crew Party <CrewPartyName> does not exist.

    Examples:
      | UserName | CrewPartyName      |
      | Allan    | Rowan's Crew Party |

    Scenario: Player in a Crew Party trying to join another
        Given a player with a RSI Handle of <UserName>
        And the player is already a member of a Crew Party named <ExistingCrewParty>
        When the player attempts to join another Crew Party named <NewCrewParty>
        Then the join request is not sent to <NewCrewParty>
        And the player receives a message You are already a member of <ExistingCrewParty>. Leave your current Crew Party to send another join request.

    Examples:
      | UserName | ExistingCrewParty  | NewCrewParty      |
      | Allan    | Rowan's Crew Party | Adam's Crew Party |