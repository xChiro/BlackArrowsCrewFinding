using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelHandlerMock : IVoiceChannelHandler
{
    public List<string> DeletedVoicedCrewIds { get; } = [];

    public Task<string> Create(string name)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task Delete(string id)
    {
        DeletedVoicedCrewIds.Add(id);
        return Task.CompletedTask;
    }

    public Task<string> CreateInvite(string channelId)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }
}