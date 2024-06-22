using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public class VoicedCrewDisbandment(
    ICrewDisbandment crewDisbandment,
    IVoiceChannelHandler voiceChannelRepository,
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    IDomainLogger domainLoggerMock, 
    IVoiceChannelCommandRepository voiceChannelCommandRepository)
    : ICrewDisbandment, ICrewDisbandmentResponse
{
    private string _crewId = string.Empty;

    public async Task Disband(ICrewDisbandmentResponse? output = null)
    {
        await crewDisbandment.Disband(this);

        try
        {
            var voiceId = await voiceChannelQueryRepository.GetVoiceChannelIdByCrewId(_crewId);

            if (voiceId is not null)
            {
                await voiceChannelRepository.Delete(voiceId);
                await voiceChannelCommandRepository.RemoveChannel(_crewId);
            }
        }
        catch (Exception e)
        {
            domainLoggerMock.Log(e, $"Voice channel of crew {_crewId} could not be deleted.");
        }
        finally
        {
            output?.SetResult(_crewId);
        }
    }

    public void SetResult(string crewId)
    {
        _crewId = crewId;
    }
}