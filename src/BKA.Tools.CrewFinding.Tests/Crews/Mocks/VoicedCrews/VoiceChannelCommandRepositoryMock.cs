using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryMock : IVoiceChannelCommandRepository
{
    public Task<string> Create(string name)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }
}