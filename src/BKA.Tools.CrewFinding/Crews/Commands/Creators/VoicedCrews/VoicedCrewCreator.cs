using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators.VoicedCrews;

public class VoicedCrewCreator(
    ICrewCreator crewCreator,
    IVoiceChannelCommandRepository voiceChannelCommandRepository,
    IVoicedCrewCommandRepository voicedCrewCommandRepository,
    IDomainLogger domainLogger)
    : ICrewCreator, ICrewCreatorResponse
{
    private string _id = string.Empty;
    private string _name = string.Empty;

    public async Task Create(CrewCreatorRequest request, ICrewCreatorResponse output)
    {
        await crewCreator.Create(request, this);

        try
        {
            var voiceChannelId = await voiceChannelCommandRepository.Create(_name);
            await voicedCrewCommandRepository.SetVoiceChannel(_id, voiceChannelId);
        }
        catch (Exception e)
        {
            domainLogger.Log(e, $"Failed to create voice channel from crew with id: {_id}");
        }
        finally
        {
            output.SetResponse(_id, _name);
        }
    }

    public void SetResponse(string id, string name)
    {
        _id = id;
        _name = name;
    }
}