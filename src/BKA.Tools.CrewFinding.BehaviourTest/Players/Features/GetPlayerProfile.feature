Feature: Get players profile
As a player I want to get my profile

    Background:
        Given the following players exist:
          | Id | Name  |
          | 1  | Rowan |

    Scenario: Get player profile
        Given I am a player logged in with id "1"
        When I get my profile
        Then I should see my profile details:
          | Id | Name  |
          | 1  | Rowan |

    Scenario: Get player profile when the player does not exist
        Given I am a player logged in with id "999"
        When I attempt get my profile
        Then I should receive an error message that the player profile does not exist