namespace BKA.Tools.CrewFinding.Channels.Exceptions;

public class CustomChannelFormatException(string channelLink) : Exception($"Invalid channel link: {channelLink}");