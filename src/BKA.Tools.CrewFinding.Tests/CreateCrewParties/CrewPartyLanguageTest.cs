using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyLanguageTest
{
    [Fact]
    public void When_creating_crew_party_without_languages_then_uses_default_languages()
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
    public void When_creating_crew_party_with_languages_successfully(params string[] languagesAbbrevs)
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
        sut.Create(captainName, totalCrew, Location.DefaultLocation(), languages);
    }
}