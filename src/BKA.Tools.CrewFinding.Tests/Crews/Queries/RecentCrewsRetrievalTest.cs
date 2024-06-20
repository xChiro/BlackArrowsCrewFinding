using System;
using System.Collections.Generic;
using System.Linq;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Queries.Recent;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Queries;

public class RecentCrewsRetrievalTest
{
    private const int CrewAgeThresholdInHours = 5;

    [Fact]
    public async void Given_There_Is_No_Recent_Crews_When_Retrieving_Recent_Crews_Then_Should_Return_Empty_List()
    {
        // Arrange
        var sut = InitializeRecentCrewsRetrieval([], CrewAgeThresholdInHours);
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
        var crews = new[] {GenerateOldCrew(10)};

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
        var expectedCrew = new Crew(Guid.NewGuid().ToString(), Player.Create("1", "Adam", 2, 16), Location.Default(),
            LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(), DateTime.UtcNow);
        var recentCrews = GenerateCrews(expectedCrew, CrewAgeThresholdInHours);

        var sut = InitializeRecentCrewsRetrieval(recentCrews, CrewAgeThresholdInHours);
        var crewsRetrievalResponseMock = new CrewsResponseMock();

        // Act
        await sut.Retrieve(crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.RecentCrews.Should().Contain(crew => crew.Id == expectedCrew.Id);
        crewsRetrievalResponseMock.RecentCrews!.Single(crew => crew.Id == expectedCrew.Id).Should()
            .BeEquivalentTo(expectedCrew);
    }

    private static Crew[] GenerateCrews(Crew recentCrew, int crewAgeThresholdInHours, int totalOldCrews = 2)
    {
        var crews = new List<Crew>();

        for (var i = 0; i < totalOldCrews; i++)
        {
            crews.Add(GenerateOldCrew(crewAgeThresholdInHours));
        }

        crews.Add(recentCrew);

        return crews.ToArray();
    }

    private static RecentCrewsRetrieval InitializeRecentCrewsRetrieval(Crew[] crews, int crewAgeThresholdInHours)
    {
        var crewQueryRepository = new CrewQueryRepositoryMock(crews: crews);
        var recentCrewsRetrieval = new RecentCrewsRetrieval(crewQueryRepository, crewAgeThresholdInHours);
        return recentCrewsRetrieval;
    }

    private static Crew GenerateOldCrew(int crewAgeThresholdInHours)
    {
        var randomId = Guid.NewGuid().ToString();
        var randomCaptainId = Guid.NewGuid().ToString();
        const string citizenName = "Adam";

        var oldCrew = new Crew(randomId, Player.Create(randomCaptainId, citizenName, 2, 16),
            Location.Default(),
            LanguageCollections.Default(), PlayerCollection.CreateEmpty(3), Activity.Default(),
            DateTime.UtcNow.AddHours(-crewAgeThresholdInHours - 1));

        return oldCrew;
    }
}