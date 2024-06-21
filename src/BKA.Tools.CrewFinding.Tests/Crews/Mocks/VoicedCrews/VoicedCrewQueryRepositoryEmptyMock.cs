using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoicedCrewQueryRepositoryEmptyMock : IVoicedCrewQueryRepository
{
    public Task<string> GetVoiceChannelIdByCrewId(string crewId)
    {
        return Task.FromResult(string.Empty);
    }
}