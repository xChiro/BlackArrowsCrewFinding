using System;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Creations;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators;

public class CrewLanguageTest
{
    [Fact]
    public void Create_Crew_Without_Languages_Uses_Default()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);
        
        // Act
        ExecuteCrewCreation(sut, 4, Array.Empty<string>());

        // Assert
        createCrewPartyResultMock.Languages.Should().BeEquivalentTo(LanguageCollections.Default());
    }

    [Theory]
    [InlineData("EN", "FR", "ES")]
    [InlineData("EN", "ES")]
    [InlineData("ES")]
    public void Create_Crew_With_Languages_Succeeds(params string[] languagesAbbrevs)
    {
        // Arrange
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        var expectedLanguages = LanguageCollections.CreateFromAbbrevs(languagesAbbrevs);

        // Act
        ExecuteCrewCreation(sut, 4, languagesAbbrevs);

        // Assert
        createCrewPartyResultMock.Languages.Should().BeEquivalentTo(expectedLanguages);
    }

    private static void ExecuteCrewCreation(ICrewCreator sut, int totalCrew,
        string[] languages)
    {
        var request = new CrewCreatorRequest(totalCrew, Location.Default(), 
            languages, Activity.Default().Name);

        sut.Create(request, new CrewCreatorResponseMock());
    }
}