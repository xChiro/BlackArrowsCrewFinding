Feature: Get an active crew by identification code

    Scenario: Obtain an active crew by identification code
        Given there is the following crews in the system
          | CrewId | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       | CurrentCrewSize |
          | 1234   | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting | 4               |
          | 3124   | Ada           | 3               | 5           | Terra   | Sol             | Terra      | New Austin       | Space explorers      | Exploration    | 3               |
        When I want to obtain the crew with identification code "1234"
        Then I should get the following crew
          | CrewId | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       | CurrentCrewSize |
          | 1234   | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting | 4               |
          
    Scenario: Obtain an active crew by identification code that does not exist
        Given there is the following crews in the system
          | CrewId | CaptainHandle | CreatedAgoHours | MaxCrewSize | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       | CurrentCrewSize |
          | 1234   | Rowan         | 1               | 4           | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting | 4               |
          | 3124   | Ada           | 3               | 5           | Terra   | Sol             | Terra      | New Austin       | Space explorers      | Exploration    | 3               |
        When I attempt to obtain the crew with identification code "999999"
        Then I should get an error message indicating that the crew does not exist
    