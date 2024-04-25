Feature: A captain has the ability to disband a Crew.

    Scenario: Captain disbands the Crew
        Given I am a player named "<UserName>"
        And I am the captain of an active Crew
        When I disband the Crew
        Then the Crew is disbanded successfully

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Captain attempts to disband a non-existent Crew
        Given I am a player named "<UserName>"
        And there is not a Crew
        When I attempt to disband the Crew
        Then the system notifies me that there is no Crew to disband

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Captain attempts to disband that is not their own
        Given I am a player named "<UserName>"
        And there is an active crew created by another player
        When I attempt to disband the Crew
        Then the system does not allow me to disband the Crew

    Examples:
      | UserName |
      | Rowan    |