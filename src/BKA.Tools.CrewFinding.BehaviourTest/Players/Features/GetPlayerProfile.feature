Feature: Get players profile
As a player I want to get my profile

    Background:
        Given the following players exist:
          | Id | Name  |
          | 1  | Rowan |

    Scenario: Get player profile without Crew
        Given I am a player logged in with id "1"
        When I get my profile
        Then I should see my profile details:
          | Id | Name  |
          | 1  | Rowan |

    Scenario: Get player profile who is a member of a Crew
        Given I am a player logged in with id "1"
        And I am a member of the crew with id "1"
        When I get my profile
        Then I should see my profile details:
          | Id | Name  | CrewId | CrewName      |
          | 1  | Rowan | 1      | Crew of Rowan |

    Scenario: Get player profile who is the captain of a Crew
        Given I am a player logged in with id "1"
        And I am the captain of an active Crew with id "1"
        When I get my profile
        Then I should see my profile details:
          | Id | Name  | CrewId | CrewName      |
          | 1  | Rowan | 1      | Crew of Rowan |

    Scenario: Get player profile when the player does not exist
        Given I am a player logged in with id "999"
        When I attempt get my profile
        Then I should receive an error message that the player profile does not exist