using System.Linq;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Tests.Crews.Values;

public class PlayerCollectionTest
{
    [Fact]
    public void Should_Not_Add_Twice_When_Adding_Existing_Player_To_Crew()
    {
        // Arrange
        var player = Player.Create("1", "Rowan");
        var members = PlayerCollection.CreateWithSingle(player, 4);

        // Act
        members.Add(player);

        // Assert
        members.Count().Should().Be(1);
    }
}