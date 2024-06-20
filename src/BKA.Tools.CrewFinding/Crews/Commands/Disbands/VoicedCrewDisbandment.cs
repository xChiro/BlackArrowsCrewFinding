using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class VoicedCrewDisbandment(
    ICrewDisbandment crewDisbandment,
    IVoiceChannelCommandRepository voiceChannelRepository,
    IDomainLogger domainLoggerMock)
    : ICrewDisbandment, ICrewDisbandmentResponse
{
    private string _crewId = string.Empty;
    private string? _voiceChannelId;

    public async Task Disband(ICrewDisbandmentResponse? output = null)
    {
        await crewDisbandment.Disband(this);

        try
        {
            if (_voiceChannelId is not null)
                await voiceChannelRepository.Delete(_voiceChannelId);
        }
        catch (Exception e)
        {
            domainLoggerMock.Log(e, $"Voice channel with id {_voiceChannelId} could not be deleted.");
        }
        finally
        {
            output?.SetResult(_crewId, _voiceChannelId);
        }
    }

    public void SetResult(string crewId, string? voiceChannelId)
    {
        _crewId = crewId;
        _voiceChannelId = voiceChannelId;
    }
}