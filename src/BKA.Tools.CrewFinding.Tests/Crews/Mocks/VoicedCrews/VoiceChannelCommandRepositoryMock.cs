using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryMock : IVoiceChannelCommandRepository
{
    public List<string> DeletedChannelIds { get; } = [];

    public Task<string> Create(string name)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task Delete(string id)
    {
        DeletedChannelIds.Add(id);
        return Task.CompletedTask;
    }
}