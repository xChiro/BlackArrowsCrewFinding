Feature: View recently created Crew Parties

    Scenario: Viewing Crew Parties created within the last hour
        Given the current time is "2023-07-29T15:00:00"
        And the following Crew Parties exist:
          | CrewPartyName         | CaptainHandle | CreationTime        | MaxCrewSize | Languages | System  | PlanetarySystem | PlanetMoon | Location         | Description            | Activity       |
          | The Stellar Hunters   | Rowan         | 2023-07-29T14:20:00 | 4           | ES, EN    | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters   | Bounty Hunting |
          | The Galactic Voyagers | Ada           | 2023-07-29T14:45:00 | 5           | EN, DE    | Terra   | Sol             | Terra      | New Austin       | Space explorers        | Exploration    |
          | The Lunar Marauders   | Kai           | 2023-07-29T13:00:00 | 3           | EN, FR    | Hurston | Stanton         | Ariel      | Lorville         | Lunar miners           | Mining         |
          | The Space Pioneers    | Eve           | 2023-07-29T13:30:00 | 6           | ES, PT    | Stanton | Crusader        | Crusader   | Port Olisar      | Intergalactic pioneers | Trade          |
        When the player searches for Crew Parties created within the last hour
        Then Allan should see the following Crew Parties:
          | CrewPartyName         | CaptainHandle | CreationTime        | MaxCrewSize | Languages | System  | PlanetarySystem | PlanetMoon | Location         | Description          | Activity       |
          | The Stellar Hunters   | Rowan         | 2023-07-29T14:20:00 | 4           | ES, EN    | Stanton | Crusader        | Crusader   | Seraphim Station | Elite bounty hunters | Bounty Hunting |
          | The Galactic Voyagers | Ada           | 2023-07-29T14:45:00 | 5           | EN, DE    | Terra   | Sol             | Terra      | New Austin       | Space explorers      | Exploration    |