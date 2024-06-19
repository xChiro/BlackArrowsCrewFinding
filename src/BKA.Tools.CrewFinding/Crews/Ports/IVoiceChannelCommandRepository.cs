namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelCommandRepository
{
    public Task<string> Create(string name);
}