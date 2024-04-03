using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Values;

public class PlayerTest
{
    [Fact]
    public void Cannot_Create_A_Player_With_Empty_Name()
    {
        // Act 
        var act = () => new Player("1", string.Empty);

        // Assert
        act.Should().Throw<NameEmptyException>();
    }

    [Theory]
    [InlineData("This is a very long name that is not valid")]
    [InlineData("This is an other very long name that is not valid")]
    public void Cannot_Create_A_Player_With_A_Long_Name(string invalidName)
    {
        // Act
        var act = () => new Player("2", invalidName);

        // Assert
        act.Should().Throw<NameLengthException>();
    }
}