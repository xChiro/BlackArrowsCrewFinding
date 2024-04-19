Feature: A player wants to create a crew party.

    Scenario: Successful creation of a Crew
        Given a player named <UserName>
        And the default MaxCrewSize is 6
        When the player creates a Crew with the following details:
          | CrewSize | Languages | System  | PlanetarySystem | Planet/Moon | Place            | Description          | Activity       |
          | 6        | ES, EN    | Stanton | Crusader        | Crusader    | Seraphim Station | Elite bounty hunters | Bounty Hunting |
        Then an empty Crew named <CrewDefaultName> is successfully created
        And the Crew contains the following details:
          | CrewSize | Languages | System  | PlanetarySystem | Planet/Moon | Place            | Description          | Activity       |
          | 6        | ES, EN    | Stanton | Crusader        | Crusader    | Seraphim Station | Elite bounty hunters | Bounty Hunting |
        And the creation date is the current date
        And <UserName> is designated as the Captain

    Examples:
      | UserName | CrewDefaultName |
      | Rowan    | Crew of Rowan  |

    Scenario: Preventing the creation of multiple active Crew Parties
        Given a player named <UserName>
        And the player already has an active Crew
        When the player attempts to create a new Crew
        Then the creation of the new Crew is prevented
        And the player receives a message indicating that the player already has an active Crew

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Creation of a Crew with default location information
        Given a player named <UserName>
        When the player attempts to create a Crew with missing location information
        Then the Crew is successfully created with the default location information

    Examples:
      | UserName | CrewName       |
      | Rowan    | The Stellar Hunters |

    Scenario: Create a Crew with missing activity information, use default activity
        Given a player named <UserName>
        When the player attempts to create a Crew with missing activity information
        Then the creation of the Crew is created with the default activity

    Examples:
      | UserName |
      | Rowan    |

    Scenario: Creating a Crew with default crew size
        Given a player named <UserName>
        And the default MaxCrewSize is <DefaultMaxCrewSize>
        When the player attempts to create a Crew with missing MaxCrewSize
        Then the Crew of <UserName> is successfully created
        And the MaxCrewSize is set to <DefaultMaxCrewSize>
        And <UserName> is designated as the Captain

    Examples:
      | UserName | DefaultMaxCrewSize |
      | Rowan    | 4                  |

    Scenario: Creating a Crew with default languages
        Given a player named <UserName>
        When the player attempts to create a Crew with missing languages
        Then the Crew is successfully created with the default languages
        And <UserName> is designated as the Captain

    Examples:
      | UserName | DefaultLanguage |
      | Rowan    | EN              |