Feature: Captain manages join requests to the Crew Party.

    Scenario: Captain approves a join request
        Given a captain with username of <CaptainHandle>
        And the captain has a not full Crew Party named <CrewPartyName>
        And a join request from <PlayerHandle> for the Crew Party <CrewPartyName>
        When the captain approves the join request
        Then <PlayerHandle> is added to <CrewPartyName>
        And a notification is sent to <PlayerHandle> saying Your request to join <CrewPartyName> has been approved.

    Examples:
      | CaptainHandle | PlayerHandle | CrewPartyName      |
      | Rowan         | Allan        | Rowan's Crew Party |

    Scenario: Captain rejects a join request

        Given a captain with username of <CaptainHandle>
        And a Crew Party named <CrewPartyName> with a captain of <CaptainHandle>
        And a join request from <PlayerHandle> for the Crew Party <CrewPartyName>
        When the captain rejects the join request
        Then <PlayerHandle> is not added to <CrewPartyName>
        And a notification is sent to <PlayerHandle> saying Your request to join <CrewPartyName> has been rejected.

    Examples:
      | CaptainHandle | PlayerHandle | CrewPartyName      |
      | Rowan         | Allan        | Rowan's Crew Party |

    Scenario: Captain does not respond to a join request in time
        Given a join request from <PlayerHandle> for the Crew Party <CrewPartyName> at <RequestedAt> minutes ago
        And the captain does not respond within the designated time frame of '30' minutes
        Then the join request is automatically declined by the system
        And a notification is sent to <PlayerHandle> saying Your request to join <CrewPartyName> has expired.

    Examples:
      | CaptainHandle | PlayerHandle | CrewPartyName      | RequestedAt |
      | Rowan         | Allan        | Rowan's Crew Party | 30          |
      | Rowan         | Allan        | Rowan's Crew Party | 40          |