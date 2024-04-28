using System;
using System.Collections.Generic;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries.Recents;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;

public class RecentCrewsRetrievalTest
{
    private const int CrewAgeThresholdInHours = 5;

    [Fact]
    public async void Given_There_Is_No_Recent_Crews_When_Retrieving_Recent_Crews_Then_Should_Return_Empty_List()
    {
        // Arrange
        var sut = InitializeRecentCrewsRetrieval(Array.Empty<Crew>(), CrewAgeThresholdInHours);
        var crewsRetrievalResponseMock = new CrewsResponseMock();

        // Act
        await sut.Retrieve(crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.RecentCrews.Should().BeEmpty();
    }

    [Fact]
    public async void Given_There_Are_Only_Old_Crews_When_Retrieving_Recent_Crews_Then_Should_Return_Empty_List()
    {
        // Arrange
        var crews = GenerateOldCrews(CrewAgeThresholdInHours);
        
        var sut = InitializeRecentCrewsRetrieval(crews, CrewAgeThresholdInHours);
        var crewsRetrievalResponseMock = new CrewsResponseMock();

        // Act
        await sut.Retrieve(crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.RecentCrews.Should().BeEmpty();
    }
    
    [Fact]
    public async void Given_There_Are_Recent_Crews_When_Retrieving_Recent_Crews_Then_Should_Return_Recent_Crews()
    {
        // Arrange
        var expectedCrew = new Crew("312412", Player.Create("1", "Adam"), new CrewName("Adam"), Location.DefaultLocation(),
            LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(), DateTime.UtcNow);
        var recentCrews = GenerateCrews(expectedCrew, CrewAgeThresholdInHours);
        
        var sut = InitializeRecentCrewsRetrieval(recentCrews, CrewAgeThresholdInHours);
        var crewsRetrievalResponseMock = new CrewsResponseMock();

        // Act
        await sut.Retrieve(crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.RecentCrews.Should().Contain(crew => crew.Id == expectedCrew.Id);
    }

    private static Crew[] GenerateCrews(Crew recentCrew, int crewAgeThresholdInHours)
    {
        var crews = new List<Crew>();
        crews.AddRange(GenerateOldCrews(crewAgeThresholdInHours));
        crews.Add(recentCrew);
        
        return crews.ToArray();
    }

    private static RecentCrewsRetrieval InitializeRecentCrewsRetrieval(Crew[] crews, int crewAgeThresholdInHours)
    {
        var crewQueryRepository = new CrewQueriesRepositoryMock(crews: crews);
        var recentCrewsRetrieval = new RecentCrewsRetrieval(crewQueryRepository, crewAgeThresholdInHours);
        return recentCrewsRetrieval;
    }

    private static Crew[] GenerateOldCrews(int crewAgeThresholdInHours)
    {
        Crew[] crews = [
            new Crew("23", Player.Create("1", "Adam"), new CrewName("Adam"), Location.DefaultLocation(),
                LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(),
                DateTime.UtcNow.AddHours(-crewAgeThresholdInHours)),
            new Crew("23", Player.Create("1", "Adam"), new CrewName("Adam"), Location.DefaultLocation(),
                LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(),
                DateTime.UtcNow.AddHours(-(crewAgeThresholdInHours + 2)))
        ];
        return crews;
    }
}