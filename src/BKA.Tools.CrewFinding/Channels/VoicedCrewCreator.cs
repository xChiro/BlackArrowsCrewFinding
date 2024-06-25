using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Channels;

public class VoicedCrewCreator(
    ICrewCreator crewCreator,
    IVoiceChannelHandler voiceChannelHandler,
    IVoiceChannelCommandRepository voiceChannelCommandRepository,
    IDomainLogger domainLogger,
    string regexPattern)
    : ICrewCreator, ICrewCreatorResponse
{
    private string _id = string.Empty;
    private string _name = string.Empty;

    public async Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        await crewCreator.Create(request, this);

        try
        {
            VoiceChannel? voiceChannelId;
            if (request is VoicedCrewCreatorRequest creatorRequest && !string.IsNullOrEmpty(creatorRequest.CustomChannelLink))
                voiceChannelId =  VoiceChannel.CreateCustom(_id, creatorRequest.CustomChannelLink, regexPattern);
            else
            {
                var channelHandlerId =  await voiceChannelHandler.Create(_name);
                voiceChannelId = new VoiceChannel(_id, channelHandlerId);
            }

            await voiceChannelCommandRepository.AddVoiceChannel(_id, voiceChannelId.VoiceChannelId);
        }
        catch (CustomChannelFormatException)
        {
            throw;
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