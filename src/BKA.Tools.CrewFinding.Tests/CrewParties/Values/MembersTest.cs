using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Values;

public class MembersTest
{
    [Fact]
    public void Attempt_To_Join_A_Player_That_Is_Already_In_The_Crew()
    {
        // Arrange
        var player = Player.Create("1", "Rowan");
        var members = Members.CreateSingle(player, 4);

        // Act
        var act = () => members.AddMember(player);

        // Assert
        act.Should().Throw<PlayerMultipleCrewsException>();
    }
}