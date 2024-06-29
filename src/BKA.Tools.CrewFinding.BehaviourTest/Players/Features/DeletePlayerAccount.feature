Feature: Players are allowed to delete their account.
    
    Scenario: A player deletes their account successfully
        Given I am a player logged in with id "1231234"
        When I delete my account
        Then I should receive a confirmation message "Your account has been deleted successfully"
        And the user should no longer exist in the system
        
    Scenario: A player tries to delete their account but it no longer exists
        Given I am a player logged in with id "1231234"
        When I delete my account
        Then I should receive an error message "User does not exist"