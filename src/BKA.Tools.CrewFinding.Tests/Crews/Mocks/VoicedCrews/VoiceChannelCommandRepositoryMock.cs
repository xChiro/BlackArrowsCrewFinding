using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelHandlerMock : IVoiceChannelHandler
{
    public List<string> DeletedVoicedCrewIds { get; } = [];
    public int CreateCallCounts { get; private set; }

    public Task<string> Create(string name)
    {
        CreateCallCounts++;
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