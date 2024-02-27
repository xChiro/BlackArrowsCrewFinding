using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyLanguageTest
{
    [Fact]
    public void Create_Crew_Party_Without_Languages_Uses_Default()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);
        
        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, Array.Empty<string>());

        // Assert
        createCrewPartyResultMock.Languages.Should().BeEquivalentTo(LanguageCollections.Default());
    }

    [Theory]
    [InlineData("EN", "FR", "ES")]
    [InlineData("EN", "ES")]
    [InlineData("ES")]
    public void Create_Crew_Party_With_Languages_Succeeds(params string[] languagesAbbrevs)
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        var expectedLanguages = LanguageCollections.CreateFromAbbrevs(languagesAbbrevs);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, languagesAbbrevs);

        // Assert
        createCrewPartyResultMock.Languages.Should().BeEquivalentTo(expectedLanguages);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew,
        string[] languages)
    {
        var request = new CrewPartyCreatorRequest(captainName, totalCrew, Location.DefaultLocation(), 
            languages, Activity.Default().Value);

        sut.Create(request);
    }
}