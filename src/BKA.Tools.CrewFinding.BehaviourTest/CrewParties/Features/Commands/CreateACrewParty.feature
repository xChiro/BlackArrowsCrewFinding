Feature: A player wants to create a crew party.

    Scenario: Successful creation of a Crew Party
        Given a player named <UserName>
        And the default MaxCrewSize is 6
        When the player creates a Crew Party named 'The Stellar Hunters' with the following details:
          | CrewSize | Languages | System  | PlanetarySystem | Planet/Moon | Place            | Description          | Activity       |
          | 6        | ES, EN    | Stanton | Crusader        | Crusader    | Seraphim Station | Elite bounty hunters | Bounty Hunting |
        Then a Crew Party with default name is successfully created for the player <UserName>
        And the Crew Party contains the following details:
          | CrewSize | Languages | System  | PlanetarySystem | Planet/Moon | Place            | Description          | Activity       |
          | 6        | ES, EN    | Stanton | Crusader        | Crusader    | Seraphim Station | Elite bounty hunters | Bounty Hunting |
        And the creation date is the current date

    Examples:
      | UserName | CrewPartyDefaultName |
      | Rowan    | Crew  Rowan    |

    Scenario: Preventing the creation of multiple active Crew Parties
        Given a player named <UserName>
        And the player already has an active Crew Party
        When the player attempts to create a new Crew Party
        Then the creation of the new Crew Party is prevented
        And the player receives a message indicating that the player already has an active Crew Party

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Creation of a Crew Party with default location information
        Given a player named <UserName>
        When the player attempts to create a Crew Party with missing location information
        Then the Crew Party is successfully created with the default location information

    Examples:
      | UserName | CrewPartyName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Create a Crew Party with missing activity information, use default activity
        Given a player named <UserName>
        When the player attempts to create a Crew Party with missing activity information
        Then the creation of the Crew Party is created with the default activity

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Creating a Crew Party with default crew size
        Given a player named <UserName>
        And the default MaxCrewSize is <DefaultMaxCrewSize>
        When the player attempts to create a Crew Party with missing MaxCrewSize
        Then the Crew Party of <UserName> is successfully created
        And the MaxCrewSize is set to <DefaultMaxCrewSize>
        And <UserName> is designated as the Captain

    Examples:
      | UserName | DefaultMaxCrewSize |
      | Rowan    | 4                  |

    Scenario: Creating a Crew Party with default languages
        Given a player named <UserName>
        When the player attempts to create a Crew Party with missing languages
        Then the Crew Party is successfully created with the default languages
        And <UserName> is designated as the Captain

    Examples:
      | UserName | DefaultLanguage |
      | Rowan    | EN              |