using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Values;

public class ActivityTest
{
    [Theory]
    [InlineData("This is a very long activity name that should throw an exception")]
    [InlineData("This is a very long activity !!")]
    public void Try_to_create_an_activity_with_a_very_long_name_throw_an_exception(string activityName)
    {
        // Act
        var act = () => Activity.Create(activityName);

        // Assert
        act.Should().Throw<ActivityNameLengthException>();
    }
    
    [Fact]
    public void Try_to_create_an_activity_with_a_to_long_description_throw_an_exception()
    {
        // Act
        var description = new string('a', 151);
        var act = () => Activity.Create("Mining", description);

        // Assert
        act.Should().Throw<ActivityDescriptionLengthException>();
    }
}