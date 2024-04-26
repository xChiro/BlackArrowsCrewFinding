Feature: View recently created Crews

    Scenario: View recently created crews
        Given I am a player named "Allan"
        And the system is configured to get the crews created in the last "5" hours
        And there is the following crews in the system
          | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description            | Activity       |
          | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters   | Bounty Hunting |
          | Ada           | 3               | 5           | Terra   | Sol             | Terra      | New Austin       | Space explorers        | Exploration    |
          | Kai           | 5               | 3           | Hurston | Stanton         | Ariel      | Lorville         | Lunar miners           | Mining         |
          | Eve           | 6               | 6           | Stanton | Crusader        | Crusader   | Port Olisar      | Intergalactic pioneers | Trade          |
        When I view the recently created crews
        Then I should see the following crews
          | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       |
          | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting |
          | Ada           | 3               | 5           | Terra   | Sol             | Terra      | New Austin       | Space explorers      | Exploration    |