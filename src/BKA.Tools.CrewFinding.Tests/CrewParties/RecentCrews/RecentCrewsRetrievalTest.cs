using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.RecentCrews;

public class RecentCrewsRetrievalTest
{
    [Fact]
    public async void Given_There_Is_No_Recent_Crews_When_Retrieving_Recent_Crews_Then_Should_Return_Empty_List()
    {
        // Arrange
        var crewQueryRepository = new CrewQueryRepositoryEmptyMock();
        var recentCrewsRetrieval = new RecentCrewsRetrieval(crewQueryRepository);
        var crewsRetrievalResponseMock = new CrewsRetrievalResponseMock();

        // Act
        await recentCrewsRetrieval.Retrieve(crewsRetrievalResponseMock);

        // Assert
        crewsRetrievalResponseMock.RecentCrews.Should().BeEmpty();
    }
}

public class CrewsRetrievalResponseMock : ICrewsRetrievalResponse
{
    public Crew[]? RecentCrews { get; private set; }

    public void SetRecentCrews(Crew[] recentCrews)
    {
        RecentCrews = recentCrews;
    }
}

public class RecentCrewsRetrieval
{
    private readonly ICrewQueryRepository _crewQueryRepository;

    public RecentCrewsRetrieval(ICrewQueryRepository crewQueryRepository)
    {
        _crewQueryRepository = crewQueryRepository;
    }

    public async Task Retrieve(ICrewsRetrievalResponse response)
    {
        var recentCrews = await _crewQueryRepository.GetCrews();
        response.SetRecentCrews(recentCrews);
    }
}

public interface ICrewsRetrievalResponse
{
    void SetRecentCrews(Crew[] recentCrews);
}

public class CrewQueryRepositoryEmptyMock : ICrewQueryRepository
{
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult<Crew?>(null);
    }

    public Task<Crew[]> GetCrews()
    {
        return Task.FromResult(Array.Empty<Crew>());
    }
}