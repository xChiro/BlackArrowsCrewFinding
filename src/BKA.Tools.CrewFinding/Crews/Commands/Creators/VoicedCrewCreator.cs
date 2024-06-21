using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public class VoicedCrewCreator(
    ICrewCreator crewCreator,
    IVoiceChannelHandler voiceChannelHandler,
    IVoiceChannelCommandRepository voiceChannelCommandRepository,
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
            var voiceChannelId = await voiceChannelHandler.Create(_name);
            await voiceChannelCommandRepository.AddVoiceChannel(_id, voiceChannelId);
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