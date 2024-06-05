Feature: Disband All Expired Crews

    Scenario: Disband all expired crews
        Given there is the following crews in the system
          | Id | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       | CurrentCrewSize | IsExpired |
          | 1  | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting | 4               | true      |
          | 2  | Ada           | 3               | 5           | Terra   | Sol             | Terra      | New Austin       | Space explorers      | Exploration    | 3               | false     |
          | 3  | Kai           | 5               | 3           | Hurston | Stanton         | Ariel      | Lorville         | Lunar miners         | Mining         | 2               | true      |
        When the system disbands all expired crews
        Then the following crews should be in the system
          | Id | CaptainHandle | CreatedAgoHours | MaxCrewSize | System | PlanetarySystem | PlanetMoon | Location   | Description     | Activity    | CurrentCrewSize |
          | 2  | Ada           | 3               | 5           | Terra  | Sol             | Terra      | New Austin | Space explorers | Exploration | 3               |
        And the following crews should be in the system logs
          | Id | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       | CurrentCrewSize |
          | 1  | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting | 4               |
          | 3  | Kai           | 5               | 3           | Hurston | Stanton         | Ariel      | Lorville         | Lunar miners         | Mining         | 2               |