using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryMock : IVoiceChannelCommandRepository
{
    public List<string> DeletedVoicedCrewsId { get; } = [];

    public Task<string> Create(string name)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task Delete(string id)
    {
        DeletedVoicedCrewsId.Add(id);
        return Task.CompletedTask;
    }

    public Task<string> CreateInvite(string channelId)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }
}