using BKA.Tools.CrewFinding.Commons.Values;

namespace BKA.Tools.CrewFinding.Tests.Commons;

public class LocationTests
{
    [Theory]
    [InlineData("", "Crusader", "Crusader", "Seraphim Station")]
    [InlineData("Stanton", "", "Crusader", "Seraphim Station")]
    [InlineData("Stanton", "Crusader", "", "Seraphim Station")]
    [InlineData("Stanton", "Crusader", "Crusader", "")]
    public void When_A_Location_Property_Is_Empty_Then_It_Should_Be_Default(string system, string planetarySystem,
        string planetMoon, string place)
    {
        // Arrange
        var expected = Location.DefaultLocation();

        // Act
        var results = new Location(system, planetarySystem, planetMoon, place);

        // Assert
        Assert.Equal(results, expected);
    }
}