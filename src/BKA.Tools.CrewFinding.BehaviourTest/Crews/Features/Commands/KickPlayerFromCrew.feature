Feature: Kick player from Crew
    Captain can now kick a player from their crew.

Scenario: Kick a player from crew successfully 
    Given I am the captain of an active Crew
    And the crew have a member with id "1234"
    When I kick the player with id "1234" from the crew
    Then the player with id "1234" is no longer in the crew
    
Scenario: Attempt to kick a player from crew when not captain
    Given I am a member of the crew with id "234"
    And the crew have a member with id "1234"
    When I kick the player with id "1234" from the crew
    Then I should see an error message indicating I am not the captain of the crew
    
Scenario: Attempt to kick a player from crew that is not in the crew
    Given I am the captain of an active Crew
    And the crew have a member with id "1234"
    When I kick the player with id "4444" from the crew
    Then I should see an error message indicating the player is not in the crew
    
 