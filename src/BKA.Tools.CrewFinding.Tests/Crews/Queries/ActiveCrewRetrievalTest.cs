using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

public class ActiveCrewRetrievalTest
{
    [Fact]
    public async Task Given_There_Is_No_Active_Crews_When_Retrieving_Active_Crews_Then_Should_Throw_CrewNotFoundException()
    {
        // Arrange
        var sut = InitializeSutActiveCrewRetrieval([]);
        var crewsRetrievalResponseMock = new CrewResponseMock();

        // Act & Assert
        await Assert.ThrowsAsync<CrewNotFoundException>(() => sut.Retrieve("1", crewsRetrievalResponseMock));
    }

    [Fact]
    public async Task Given_There_Is_Active_Crews_When_Is_Looking_For_An_Unexisting_Crew_Then_Should_Throw_CrewNotFoundException()
    {
        // Arrange
        var crew = CreateCrew("1");
        var sut = InitializeSutActiveCrewRetrieval([crew]);
        var crewsRetrievalResponseMock = new CrewResponseMock();

        // Act & Assert
        await Assert.ThrowsAsync<CrewNotFoundException>(() => sut.Retrieve("3", crewsRetrievalResponseMock));
    }

    [Fact]
    public async void Given_There_Is_Active_Crews_When_Looking_For_An_Existing_Crew_Then_Should_Return_The_Crew()
    {
        // Arrange
        var expectedCrew = CreateCrew("1");
        var crew = CreateCrew("2");
        
        var sut = InitializeSutActiveCrewRetrieval([expectedCrew, crew]);
        var crewsRetrievalResponseMock = new CrewResponseMock();

        // Act
        await sut.Retrieve("1", crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.Crew.Should().BeEquivalentTo(expectedCrew);
    }

    public static IActiveCrewRetrieval InitializeSutActiveCrewRetrieval(List<Crew> crews)
    {
        var crewsQueryRepositoryMock = CreateCrewsQueryRepositoryMock(crews);
        var sut = new ActiveCrewRetrieval(crewsQueryRepositoryMock);
        
        return sut;
    }

    private static CrewQueriesRepositoryMock CreateCrewsQueryRepositoryMock(List<Crew> crews)
    {
        var crewsQueryRepositoryMock = new CrewQueriesRepositoryMock(crews: crews);
        
        return crewsQueryRepositoryMock;
    }

    private static Crew CreateCrew(string id)
    {
        return new Crew(id, Player.Create("1", "Adam"), Location.Default(),
            LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(), DateTime.UtcNow);
    }
}