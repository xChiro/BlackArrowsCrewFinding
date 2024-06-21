using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoicedCrewQueryRepositoryMock(string voiceChannelId) : IVoicedCrewQueryRepository
{
    public Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        return Task.FromResult(voiceChannelId)!;
    }
}