Feature: View recently created Crews

    Scenario: View recently created crews
        Given I am a player named "Allan"
        And the system is configured to get the crews created in the last "5" hour
        And there is the following crews in the system
          | CrewPartyName         | CaptainHandle | CreatedAgoHours | MaxCrewSize | Languages | System  | PlanetarySystem | PlanetMoon | Location         | Description            | Activity       |
          | The Stellar Hunters   | Rowan         | 1               | 4           | ES, EN    | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters   | Bounty Hunting |
          | The Galactic Voyagers | Ada           | 3               | 5           | EN, DE    | Terra   | Sol             | Terra      | New Austin       | Space explorers        | Exploration    |
          | The Lunar Marauders   | Kai           | 5               | 3           | EN, FR    | Hurston | Stanton         | Ariel      | Lorville         | Lunar miners           | Mining         |
          | The Space Pioneers    | Eve           | 6               | 6           | ES, PT    | Stanton | Crusader        | Crusader   | Port Olisar      | Intergalactic pioneers | Trade          |
        When I view the recently created crews
        Then I should see the following crews
          | CrewPartyName         | CaptainHandle | CreatedAgoHours | MaxCrewSize | Languages | System  | PlanetarySystem | PlanetMoon | Location         | Description            | Activity       |
          | The Stellar Hunters   | Rowan         | 1               | 4           | ES, EN    | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters   | Bounty Hunting |
          | The Galactic Voyagers | Ada           | 3               | 5           | EN, DE    | Terra   | Sol             | Terra      | New Austin       | Space explorers        | Exploration    |