namespace BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;

public class VoiceChannelDocument
{
    public string Id { get; set; }
    public string CrewId { get; set; }
    public string ChannelId { get; set; }
    public DateTime CreateAt { get; set; }
}