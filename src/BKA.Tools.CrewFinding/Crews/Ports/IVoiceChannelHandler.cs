namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelHandler
{
    public Task<string> Create(string name);
    
    public Task Delete(string id);
    
    public Task<string> CreateInvite(string channelId);
}