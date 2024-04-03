using System;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class CrewPartyLanguageTest
{
    [Fact]
    public void Create_Crew_Party_Without_Languages_Uses_Default()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);
        
        // Act
        ExecuteCrewCreation(sut, 4, Array.Empty<string>());

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
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        var expectedLanguages = LanguageCollections.CreateFromAbbrevs(languagesAbbrevs);

        // Act
        ExecuteCrewCreation(sut, 4, languagesAbbrevs);

        // Assert
        createCrewPartyResultMock.Languages.Should().BeEquivalentTo(expectedLanguages);
    }

    private static void ExecuteCrewCreation(ICrewPartyCreator sut, int totalCrew,
        string[] languages)
    {
        var request = new CrewPartyCreatorRequest(Guid.NewGuid().ToString(), totalCrew, Location.DefaultLocation(), 
            languages, Activity.Default().Name);

        sut.Create(request, new CrewPartyCreatorResponseMock());
    }
}